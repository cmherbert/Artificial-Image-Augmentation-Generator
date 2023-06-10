namespace ArtificalAugmentationGenerator.Components.Options
{
    public class OptionValidationReturn
    {
        private bool _success;
        private string _message;

        public bool Success => _success;
        public string Message => _message;

        public OptionValidationReturn(bool success, string message)
        {
            _success = success;
            _message = message;
        }

        public OptionValidationReturn(bool success)
        {
            _success=success;
            _message = success ? "OK" : "Failure reason not provided";
        }
    }
}