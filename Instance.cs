namespace ClientClasses.Instance
{
    class GoogleInstance
    {
        public string? name { get; set; }
        public NetworkInterface[]? networkInterfaces { get; set; }
    }

    class NetworkInterface
    {
        public string? name { get; set; }
        public string? networkIP { get; set; }
        public string? network { get; set; }
        public string? subnetwork { get; set; }
    }
}
