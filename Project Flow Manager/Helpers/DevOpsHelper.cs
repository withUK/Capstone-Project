using System.Net.Http.Headers;
using System.Text;

namespace ProjectFlowManagerProject_Flow_Manager.Helpers
{
    public static class DevOpsHelper
    {
        private static Uri uri = new Uri("https://dev.azure.com/PW-LCC");
        private static string personalAccessToken = "zuweitbmanzsdf5o2jtudw7hyfxztbaxxi2ncpxgzfpor5qyt62q";

        public static async Task<bool> CreateProject(string projectName, string? projectDescription)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                        Convert.ToBase64String(
                            Encoding.ASCII.GetBytes(
                                string.Format("{0}:{1}", "", personalAccessToken))));

                    var req = new ProjectRequest
                    {
                        name = projectName,
                        description = projectDescription,
                        visibility = 0,
                        capabilities = new Capabilities
                        {
                            versioncontrol = new Versioncontrol { sourceControlType = "Git" },
                            processTemplate = new ProcessTemplate
                            {
                                templateTypeId = "b8a3a935-7e91-48b8-a94c-606d37c3e9f2"
                            }
                        }
                    };

                    var result = await client.PostAsJsonAsync($"{uri}/_apis/projects?api-version=6.0", req);

                    Console.WriteLine(result.StatusCode);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return true;
        }

        private class Versioncontrol
        {
            public string sourceControlType { get; set; }
        }

        private class ProcessTemplate
        {
            public string templateTypeId { get; set; }
        }

        private class Capabilities
        {
            public Versioncontrol versioncontrol { get; set; }
            public ProcessTemplate processTemplate { get; set; }
        }

        private class ProjectRequest
        {
            public string name { get; set; }
            public string description { get; set; }
            public int visibility { get; set; }
            public Capabilities capabilities { get; set; }
        }
    }
}