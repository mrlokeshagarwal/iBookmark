using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBookmark.Model.AggregatesModel.ContainerAggregate
{
    public interface IContainerRepository
    {
        Task<int> InsertUpdateContainerAsync(ContainerModel container);
        Task<IEnumerable<ContainerModel>> GetContainersAsync(int userId);
    }
}
