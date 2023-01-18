using System;
using System.Text;

namespace Polaroider
{
    /// <summary>
    /// Exception that gets thrown when the Testmethod is not found
    /// </summary>
    public class TestMethodNotFoundException : Exception
    {
        /// <summary>
        /// Exception that gets thrown when the Testmethod is not found
        /// </summary>
        public TestMethodNotFoundException()
        {
            var msg = new StringBuilder()
                .AppendLine("Polaroider could not find the file containing the Test.")
                .AppendLine("Please ensure the following is configured in the Testproject:")
                .AppendLine("- Enable the generation of *.pdb files")
                .AppendLine("- Disable Optimize code for build. This can be set in the Project Properties or through <Optimize>False</Optimize> in the *.csproj file of the Testproject")
                .AppendLine("- When using Live Unit Testing make sure the checkbox for 'Enable debug symbol and xml documentation comment generation' is enabled in the Visual Studio Options.");

            Message = msg.ToString();
        }

        /// <summary>
        /// Gets the message of the Exception
        /// </summary>
        public override string Message { get; }
    }
}
