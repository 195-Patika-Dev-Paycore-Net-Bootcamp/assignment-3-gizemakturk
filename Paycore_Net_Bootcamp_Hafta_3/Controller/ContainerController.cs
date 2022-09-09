using  Paycore_Net_Bootcamp_Hafta_3.Context;
using  Paycore_Net_Bootcamp_Hafta_3.Models;

using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Paycore_Net_Bootcamp_Hafta_3.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContainerController : ControllerBase
    {

        private readonly IMapperSession<Container> sessionContainer;
        


        public ContainerController(IMapperSession<Container> sessionContainer)
        {
            this.sessionContainer = sessionContainer;

        }

        //Get all containers
        // GET: api/<ContainerController>
        [HttpGet]
        public IEnumerable<Container> Get()
        {
            return sessionContainer.Entities;
        }
        //get container by given id
        // GET api/<ContainerController>/5
        [HttpGet("{id}")]
        public Container Get(int id)
        {
            return sessionContainer.Entities.Where(x => x.Id == id).FirstOrDefault();
        }
        //create new container 
        // POST api/<ContainerController>
        [HttpPost]
        public void Post([FromBody] Container container)
        {
            try
            {
                sessionContainer.BeginTransaction();
                sessionContainer.Save(container);
                sessionContainer.Commit();
            }
            catch (Exception ex)
            {
                sessionContainer.Rollback();
                Log.Error(ex, "containers Insert Error");
            }
            finally
            {
                sessionContainer.CloseTransaction();
            }
        }
        //update existing container
        // PUT api/<ContainerController>/5
        [HttpPut]
        public ActionResult<Container> Put([FromBody] Container request)
        {
            Container container = sessionContainer.Entities.Where(x => x.Id == request.Id).FirstOrDefault();
            if (container == null)
            {
                return NotFound();
            }

            try
            {
                sessionContainer.BeginTransaction();
                //update all attribute except vehicle id
                container.ContainerName = request.ContainerName;
                container.Longitude = request.Longitude;
                container.Latitude = request.Latitude;



                sessionContainer.Update(container);

                sessionContainer.Commit();
            }
            catch (Exception ex)
            {
                sessionContainer.Rollback();
                Log.Error(ex, "container Insert Error");
            }
            finally
            {
                sessionContainer.CloseTransaction();
            }


            return Ok();
        }
        //delete existing contanier
        [HttpDelete("{id}")]
        public ActionResult<Container> Delete(int id)
        {
            Container container = sessionContainer.Entities.Where(x => x.Id == id).FirstOrDefault();
            if (container == null)
            {
                return NotFound();
            }

            try
            {
                sessionContainer.BeginTransaction();
                sessionContainer.Delete(container);
                sessionContainer.Commit();
            }
            catch (Exception ex)
            {
                sessionContainer.Rollback();
                Log.Error(ex, "container Insert Error");
            }
            finally
            {
                sessionContainer.CloseTransaction();
            }

            return Ok();
        }
    }
}


