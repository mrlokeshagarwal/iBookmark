namespace iBookmark.Model.AggregatesModel.ContainerAggregate
{
    public class ContainerModel
    {
        private string _containerName;

        public int ContainerId { get; set; }

        public string ContainerName { get; set; }

        public void Add(string containerName)
        {
            _containerName = containerName;
        }
    }
}
