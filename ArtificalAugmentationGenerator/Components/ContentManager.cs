using ArtificalAugmentationGenerator.Plugins;
using ArtificalAugmentationGenerator.Plugins.Presets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ArtificalAugmentationGenerator.Components
{
    internal static class ContentManager
    {
        static List<IContentPackage> _contents = new List<IContentPackage>();
        static List<string> _problems = new List<string>();

        internal static IReadOnlyList<AugmentationPackage> Augmentations => _contents.Where(x => x is AugmentationPackage).Cast<AugmentationPackage>().ToList().AsReadOnly();
        internal static IReadOnlyList<PresetTypePackage> PresetTypes => _contents.Where(x => x is PresetTypePackage).Cast<PresetTypePackage>().ToList().AsReadOnly();
        internal static IReadOnlyList<ProcessorPackage> ProcessorPackages => _contents.Where(x => x is ProcessorPackage).Cast<ProcessorPackage>().ToList().AsReadOnly();
        internal static bool LoadFailed => _problems.Count > 0;
        internal static string[] Problems => _problems.ToArray();

        static ContentManager()
        {
            try
            {
                //Load Assembly Effects
                foreach (var type in Assembly.GetExecutingAssembly().GetTypes().Where(x => x.IsClass))
                {
                    try
                    {
                        if (typeof(Augmentation).IsAssignableFrom(type) && !type.IsAbstract)
                            _contents.Add(new AugmentationPackage((Augmentation)Activator.CreateInstance(type), type.Assembly.Location, true) as IContentPackage);
                        else if (typeof(IPresetType).IsAssignableFrom(type) && !type.IsAbstract)
                            _contents.Add(new PresetTypePackage((IPresetType)Activator.CreateInstance(type), type.Assembly.Location, true) as IContentPackage);
                        else if (typeof(IAugmentationProcessor).IsAssignableFrom(type) && !type.IsAbstract)
                        {
                            _contents.Add(new ProcessorPackage(type, type.Assembly.Location, true) as IContentPackage);
                        }
                    }
                    catch (Exception ex)
                    {
                        _problems.Add($"Could not load type {type.FullName} from {type.Assembly.FullName}.\t {ex.ToString()}");
                    }
                }
                foreach (var dll in Directory.GetFiles(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Plugins"), "*.dll", SearchOption.AllDirectories))
                {
                    try
                    {
                        var ass = Assembly.LoadFile(dll);
                        foreach (var type in ass.GetTypes().Where(x => x.IsClass))
                        {
                            try
                            {
                                if (typeof(IAugmentation).IsAssignableFrom(type))
                                {
                                    //create and test (default) processor activation
                                    var augmentation = (IAugmentation)Activator.CreateInstance(type);
                                    Activator.CreateInstance(augmentation.Processor, augmentation);
                                    _contents.Add(new AugmentationPackage(augmentation, dll, false) as IContentPackage);
                                }
                                else if (typeof(IPresetType).IsAssignableFrom(type))
                                    _contents.Add(new PresetTypePackage((IPresetType)Activator.CreateInstance(type), dll, false) as IContentPackage);
                                else if (typeof(IAugmentationProcessor).IsAssignableFrom(type))
                                {
                                    _contents.Add(new ProcessorPackage(type, dll, false) as IContentPackage);
                                }
                            }
                            catch (Exception ex)
                            {
                                _problems.Add($"Could not load type {type.FullName} from {dll} .\t {ex.ToString()}");
                            }
                        }
                    }
                    catch
                    {
                        //Invalid Assembly
                    }
                }
            }
            catch (Exception ex)
            {
                _problems.Add(ex.ToString());
            }
        }
    }
}
