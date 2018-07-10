using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iBookmark.Model.AggregatesModel.ContainerAggregate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace iBookmark.API.Controllers
{
    [Produces("application/json")]
    [Route("containers")]
    public class ContainersController : Controller
    {
        private IContainerRepository _containerRepository;

        public ContainersController(IContainerRepository containerRepository)
        {
            _containerRepository = containerRepository;
        }

        /// <summary>
        /// Returns list of containers available for provided user
        /// </summary>
        /// <param name="userId">user id</param>
        /// <returns></returns>
        [HttpGet("{userId}")]
        [ProducesResponseType(typeof(List<ContainerModel>), 200)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> GetContainersAsync(int userId)
        {
            var containers = await _containerRepository.GetContainersAsync(userId).ConfigureAwait(false);
            if (containers != null && containers.Count() > 0)
                return new ObjectResult(containers);
            else
                return NoContent();
        }

        /// <summary>
        /// Inserts new container into system
        /// </summary>
        /// <param name="container">ContainerName, UserId</param>
        /// <returns></returns>
        [HttpPost("")]
        [ProducesResponseType(typeof(int), 200)]
        public async Task<int> InsertContainer([FromBody]ContainerModel container)
        {
            container.ContainerId = 0;
            var containerId = await _containerRepository.InsertUpdateContainerAsync(container).ConfigureAwait(false);
            return containerId;
        }

        /// <summary>
        /// Update information i.e. ContainerName for provided containerId
        /// </summary>
        /// <param name="container">ContainerName, UserId</param>
        /// <param name="containerId"></param>
        /// <returns></returns>
        [HttpPut("{containerId}")]
        [ProducesResponseType(typeof(int), 200)]
        public async Task<int> UpdateContainer([FromBody]ContainerModel container, int containerId)
        {
            container.ContainerId = containerId;
            containerId = await _containerRepository.InsertUpdateContainerAsync(container).ConfigureAwait(false);
            return containerId;
        }
    }
}