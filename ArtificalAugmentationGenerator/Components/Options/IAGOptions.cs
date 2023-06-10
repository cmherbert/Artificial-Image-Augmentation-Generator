using ArtificalAugmentationGenerator.Components.Options.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArtificalAugmentationGenerator.Components.Options
{
    /// <summary>
    /// Provides command line switches for program
    /// </summary>
    internal class IAGOptions : BaseProgramOptions
    {
        #region Main Options
        [Option("interactive", OptionType.Switch, false, "Show Image Artefact Generator User Interface")]
        public bool Interactive { get; set; } = false;
        [Option("workersuseprocess", OptionType.Switch, false, "Workers use a unique process rather than a unique thread")]
        public bool WorkersUseProcess { get; set; } = false;

        [Option("list", OptionType.Multiple, new string[0], "List Plugin types found by Assembly")]
        public string[] ListEffects { get; set; } = null;

        [Option("preserve-fs", OptionType.Switch, false, "Recreate folder structure from Source Folder in output folders. Does nothing unless recursive argument is used")]
        public bool PreserveFolderStructure { get; set; } = false;
        [Option("recursive", OptionType.Switch, false, "Instructs program to search beyond top level of Source Folder. Use 'Preserve-fs' to keep structure in output")]
        public bool Recursive { get; set; } = false;
        [Option("missingonly", OptionType.Switch, false, "Instructs program to only process missing images")]
        public bool MissingOnly { get; set; } = false;
        [Option("overrideoutputrestrictions", OptionType.Switch, false, "Instructs program to ignore output restrictions when output is redirected. Not recommened on Windows")]
        public bool OverrideOutputRestrictions { get; set; } = false;

        [Option("help", OptionType.Switch, false, "Show Image Artefact Generator help")]
        public bool Help { get; set; } = false;
        [Option("workers", OptionType.Single, 2, "Specify degree of parallelism. Default 2")]
        public int Workers { get; set; } = 2;

        [Option("source-folder", OptionType.Single, null, "Directory containing unmodified source images")]
        public string SourceFolder { get; set; } = null;

        [Option("source-filter", OptionType.Single, null, "Filename filter for source images (Wildcard)")]
        public string SourceFilter { get; set; } = null;

        [Option("path-filter", OptionType.Single, null, "Filepath filter for source images (Regex)")]
        public string PathFilter { get; set; } = null;

        [Option("source-labels", OptionType.Single, null, "Labels for source images, used for large randomisation")]
        public string SourceLabel { get; set; } = null;
        [Option("source-filterfiles", OptionType.Multiple, new string[0], "Filter files with whitelited filenames")]
        public string[] SourceFilterFiles { get; set; } = new string[0];

        [Option("augmentations", OptionType.Multiple, new string[] { }, "Collection of augmentations to be applied")]
        public string[] Augmentations { get; set; } = new string[0];

        [Option("presets", OptionType.Multiple, new string[0], "Collection of paths to preset XML files. Program will apply each enabled preset for specified augmentation. You can disable a preset by either commenting out a preset or adding attribute 'enabled=\"false\"' to 'Preset' node.")]
        public string[] Presets { get; set; } = new string[0];

        [Option("output-folder", OptionType.Single, null, "Root directory for output, folder structure will be created for each effect and preset")]
        public string OutputFolder { get; set; } = null;
        #endregion
        #region HiddenOptions
        [Option("wp_id", OptionType.Single, -1, "Worker Process -- Worker ID, specifies process ID", true)]
        public int WP_ID { get; set; } = -1;
        [Option("wp_parentid", OptionType.Single, -1, "Worker Process -- Parent process ID, worker should terminate on parent terminating ", true)]
        public int WP_Parent { get; set; } = -1;
        [Option("wp_port", OptionType.Single, -1, "Worker Process -- Update Server Port ", true)]
        public int WP_Port { get; set; } = -1;


        #endregion
        /// <summary>
        /// Performs input validation on Image Augmentation Generator 
        /// </summary>
        /// <returns>OptionValidationReturn object with information on success / failure</returns>
        protected override OptionValidationReturn ValidateInput()
        {
            if (Help)   //Ignore other switches, will show help
                return new OptionValidationReturn(false, "Image Artefact Generator Help");
            else if (Interactive)   //Ignore other switches, will start interactive user interface
                return new OptionValidationReturn(true);
            else if (ListEffects != null)    //Ignore other switches, will list loaded types
                return new OptionValidationReturn(true);
            else if (Workers <= 0)  //Ignore other switches, number of workers is invalid
                return new OptionValidationReturn(false, "Invalid number of workers");
           
            else if (WP_ID > -1 && WP_Parent == -1 || WP_ID == -1 && WP_Parent > -1)
            {   //Ignore other switches, hidden switches WP_ID & WP_Parent are invalid. Used for multi-processing
                return new OptionValidationReturn(false, "Invalid WorkerProcess setup. Both Worker ID and Worker Parent must be supplied");
            }
            else
            {
                if (SourceFolder == null) //Check source folder is supplied
                    return new OptionValidationReturn(false, "Source folder not specified");
                else if (OutputFolder == null) //Check output folder is supplied
                    return new OptionValidationReturn(false, "Output Folder not specified");

                if (!Directory.Exists(SourceFolder)) //Check source folder exists
                    return new OptionValidationReturn(false, "Source folder could not be found");
            }
            //All switches valid
            return new OptionValidationReturn(true);
        }


    }
}
