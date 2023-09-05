using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace CleanArchitecture.AcceptanceTests
{
    public class TestHostEnvironment : IHostEnvironment
    {
        public string EnvironmentName { get; set; } = Environments.Development;
        public string ApplicationName { get; set; } = typeof(TestHostEnvironment).Namespace;
        public string ContentRootPath { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IFileProvider ContentRootFileProvider { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
