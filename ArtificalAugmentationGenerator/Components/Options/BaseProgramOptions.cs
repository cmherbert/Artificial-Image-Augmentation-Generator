using ArtificalAugmentationGenerator.Components.Options.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ArtificalAugmentationGenerator.Components.Options
{
    public abstract class BaseProgramOptions
    {
        public static T CreateOptionsClass<T>(string[] arguments) where T : BaseProgramOptions
        {
            T options = Activator.CreateInstance<T>();
            List<string> _visitedArgs = new List<string>();
            Dictionary<string, Tuple<PropertyInfo, OptionAttribute>> _properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(x => x.GetCustomAttribute<OptionAttribute>() != null).Select(x => new Tuple<PropertyInfo, OptionAttribute>(x, x.GetCustomAttribute<OptionAttribute>())).ToDictionary(x => x.Item2.Name, x => x);
            string _targetArg = null;

            foreach (var arg in arguments)
            {
                if (arg.StartsWith("--") /*|| arg.StartsWith("/")*/)
                {
                    var optname = arg.StartsWith("--") ? arg.Substring(2) : arg.Substring(1);
                    var _opt = _properties.Values.ToList().Find(x => x.Item2.Name.Equals(optname, StringComparison.CurrentCulture));
                    if (_opt == null)
                    {
                        options.PrintHelp<T>($"Option '{optname}' not recognised");

                        return null;
                    }


                    if (_properties[optname].Item2.OptionType == OptionType.Switch)
                    {
                        _properties[optname].Item1.SetValue(options, true);
                        _targetArg = null;
                    }
                    else
                    {
                        _targetArg = optname;
                        //Init property if null && has default
                        if (_properties[optname].Item1.GetValue(options) == null && _properties[optname].Item2.DefaultValue != null)
                            _properties[optname].Item1.SetValue(options, _properties[optname].Item2.DefaultValue);

                    }
                    continue;
                }
                else
                {
                    if (_targetArg == null)
                    {
                        options.PrintHelp<T>($"'{_targetArg}' is unrecognised in its current position");
                        return null;
                    }
                    if (_visitedArgs.Contains(_targetArg) && _properties[_targetArg].Item2.OptionType != OptionType.Multiple)
                    {
                        options.PrintHelp<T>($"Option '{_targetArg}' does not support being used more than once");
                        return null;
                    }
                    else
                    {
                        _visitedArgs.Add(_targetArg);

                        if (_properties[_targetArg].Item2.OptionType == OptionType.Multiple)
                        {
                            //Check type is array of enumerable
                            if (_properties[_targetArg].Item1.PropertyType.IsArray)
                            {
                                if (_properties[_targetArg].Item1.PropertyType.GetElementType() != typeof(string))
                                {
                                    options.PrintHelp<T>($"Usage of non String[] arrays is not currently supported. This is a developer problem");
                                    return null;
                                }
                                else
                                {
                                    List<string> x = ((string[])_properties[_targetArg].Item1.GetValue(options)).ToList();
                                    x.Add(arg.ToString().Trim());
                                    _properties[_targetArg].Item1.SetValue(options, x.ToArray());
                                }
                            }
                            else if (_properties[_targetArg].Item1.PropertyType.IsAssignableFrom(typeof(ICollection<>)))
                            {
                                ICollection<object> x = _properties[_targetArg].Item1.GetValue(options) as ICollection<object>;
                                x.Add(Cast(arg, _properties[_targetArg].Item1.PropertyType));
                                _properties[_targetArg].Item1.SetValue(options, x);
                            }
                            else
                            {
                                options.PrintHelp<T>($"Option '{_targetArg}' cannot be mapped to {_properties[_targetArg].Item1.PropertyType}. This issue must be fixed by the programs' developer");
                                return null;
                            }


                        }
                        else if (_properties[_targetArg].Item2.OptionType == OptionType.Single)
                        {
                            _properties[_targetArg].Item1.SetValue(options, Cast(arg, _properties[_targetArg].Item1.PropertyType));
                        }
                        else
                        {
                            options.PrintHelp<T>($"Option '{_targetArg}' does not support parameters");
                            return null;
                        }
                    }
                }
            }
            var vr = options.ValidateInput();
            if (!vr.Success)
            {
                options.PrintHelp<T>(vr.Message);

                return null;
            }
            return options;

        }

        private static object Cast(string x, Type target)
        {
            return Convert.ChangeType(x, target.IsArray ? target.GetElementType() : target.GenericTypeArguments.Count() > 0 ? target.GenericTypeArguments[0] : target);
        }

        protected abstract OptionValidationReturn ValidateInput();
        public virtual void PrintHelp<T>(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine();
            Console.WriteLine($"{System.IO.Path.GetFileName(Assembly.GetEntryAssembly().Location)} [Options / Switches]");
            Console.WriteLine();

            List<OptionAttribute> _properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(x => x.GetCustomAttribute<OptionAttribute>() != null).Select(x => x.GetCustomAttribute<OptionAttribute>()).ToList();
            int l = 0;

            Console.WriteLine("Switches:");

            _properties.ForEach(x => l = Math.Max(l, x.Name.Length));

            foreach (var prop in _properties.Where(x => x.OptionType == OptionType.Switch && !x.IsHidden))
            {
                Console.Write($"\t--{prop.Name + new string(' ', l - prop.Name.Length)} {(prop.OptionType == OptionType.Multiple ? "[...]" : "     ")} ");
                int x = Console.CursorLeft;
                int w = Console.BufferWidth - x - 1;
                int r = 0;
                while (r < prop.HelpText.Length)
                {
                    Console.SetCursorPosition(x, Console.CursorTop);
                    var hs = prop.HelpText.Substring(r, Math.Min(prop.HelpText.Length - r, w));
                    Console.WriteLine(hs);
                    r += hs.Length;
                }
                //Console.WriteLine($"\t/{prop.Name.ToUpper() + new string(' ', l - prop.Name.Length)} {(prop.OptionType == OptionType.Multiple ? "[...]" : "     ")} {prop.HelpText}");
            }
            Console.WriteLine();
            Console.WriteLine("Options:");



            foreach (var prop in _properties.Where(x => x.OptionType != OptionType.Switch && !x.IsHidden))
            {
                Console.Write($"\t--{prop.Name + new string(' ', l - prop.Name.Length)} {(prop.OptionType == OptionType.Multiple ? "[...]" : "[   ]")} ");
                int x = Console.CursorLeft;
                int w = Console.BufferWidth - x - 1;
                int r = 0;
                while (r < prop.HelpText.Length)
                {
                    Console.SetCursorPosition(x, Console.CursorTop);
                    var hs = prop.HelpText.Substring(r, Math.Min(prop.HelpText.Length - r, w));
                    Console.WriteLine(hs);
                    r += hs.Length;
                }
                //Console.WriteLine($"\t/{prop.Name.ToUpper() + new string(' ', l - prop.Name.Length)} {(prop.OptionType == OptionType.Multiple ? "[...]" : "     ")} {prop.HelpText}");
            }
            Console.WriteLine();
            Console.WriteLine();
            return;
        }

        internal Dictionary<string, object> GetConfiguration<T>(T options) where T : BaseProgramOptions
        {
            Dictionary<string, object> config = new Dictionary<string, object>();
            List<Tuple<OptionAttribute, object>> _properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(x => x.GetCustomAttribute<OptionAttribute>() != null).Select(x => new Tuple<OptionAttribute, object>(x.GetCustomAttribute<OptionAttribute>(), x.GetValue(options))).ToList();
            foreach (var prop in _properties)
            {
                if (!prop.Item1.IsHidden || !(prop.Item1.DefaultValue.Equals(prop.Item2)))
                    config.Add(prop.Item1.Name, prop.Item2);
            }
            return config;
        }
    }
}
