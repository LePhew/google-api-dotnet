using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

//Google's
using Google.Cloud.Compute.V1;
using Google.Cloud.Container.V1;
using Google.Cloud.Storage.V1;
using Google.Cloud.ResourceManager.V3;
using Google.Api.Gax;
using Google.Api.Gax.ResourceNames;

//This App's context Libraries
using ClientClasses.Instance;


var projectId = "exemplary-tide-348417";
var zone = "us-east1-b";

//VM instances
InstancesClient instancesClient = InstancesClient.Create();

ListInstancesRequest request = new ListInstancesRequest
{
    Zone = zone,
    Project = projectId,
    ReturnPartialSuccess = false,
};
// Make the request
PagedEnumerable<InstanceList, Instance> response = instancesClient.List(request);

// Iterate over all response items, lazily performing RPCs as required
Console.WriteLine("GCP Instances List.");
Console.WriteLine("================================");

foreach (Instance item in response)
{
    GoogleInstance? vm = JsonSerializer.Deserialize<GoogleInstance>(item.ToString());
    Console.WriteLine(JsonSerializer.Serialize(vm));
}

Console.WriteLine("GKE List.");
Console.WriteLine("================================");
//Google Kubernetes Clusters
ClusterManagerClient client = ClusterManagerClient.Create();
// You can list clusters in a single zone, or specify "-" for all zones.
LocationName location = new LocationName(projectId, locationId: "-");
ListClustersResponse zones = client.ListClusters(location.ToString());
foreach (Cluster cluster in zones.Clusters)
{
    Console.WriteLine($"Cluster {cluster.Name} in {cluster.Location}");
}

Console.WriteLine("GCP Networks List.");
Console.WriteLine("================================");
//Google VPC
NetworksClient networksClient = NetworksClient.Create();
ListNetworksRequest networksRequest = new ListNetworksRequest
{
    Project = projectId,
};
PagedEnumerable<NetworkList, Network> networkResponse = networksClient.List(networksRequest);
foreach (Network network in networkResponse)
{
    Console.WriteLine(network);
}

Console.WriteLine("GCP Buckets List.");
Console.WriteLine("================================");

//Google Storage
var storage = StorageClient.Create();
var buckets = storage.ListBuckets(projectId);
Console.WriteLine("Buckets:");
foreach (var bucket in buckets)
{
    Console.WriteLine($"{bucket.Name} located in {bucket.Location}");
}

Console.WriteLine("Projects List.");
Console.WriteLine("================================");

var projectsClient = Google.Cloud.ResourceManager.V3.ProjectsClient.Create();
ListProjectsRequest listRequest = new ListProjectsRequest
{
    PageSize = 10,
};
var projects = projectsClient.ListProjects(listRequest);
foreach (var project in projects)
{
    Console.WriteLine(project);
}