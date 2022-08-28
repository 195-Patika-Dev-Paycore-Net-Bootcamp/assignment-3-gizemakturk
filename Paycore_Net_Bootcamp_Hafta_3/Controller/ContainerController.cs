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

        private readonly IMapperSession session;

        public ContainerController(IMapperSession session)
        {
            this.session = session;
        }


        // GET: api/<ContainerController>
        [HttpGet]
        public IEnumerable<Container> Get()
        {
            return session.Containers;
        }

        // GET api/<ContainerController>/5
        [HttpGet("{id}")]
        public Container Get(int id)
        {
            return session.Containers.Where(x => x.Id == id).FirstOrDefault();
        }

        // POST api/<ContainerController>
        [HttpPost]
        public void Post([FromBody] Container container)
        {
            try
            {
                session.BeginTransaction();
                session.Save(container);
                session.Commit();
            }
            catch (Exception ex)
            {
                session.Rollback();
                Log.Error(ex, "containers Insert Error");
            }
            finally
            {
                session.CloseTransaction();
            }
        }

        // PUT api/<ContainerController>/5
        [HttpPut]
        public ActionResult<Container> Put([FromBody] Container request)
        {
            Container container = session.Containers.Where(x => x.Id == request.Id).FirstOrDefault();
            if (container == null)
            {
                return NotFound();
            }

            try
            {
                session.BeginTransaction();

                container.ContainerName = request.ContainerName;
                container.Longitude = request.Longitude;
                container.Latitude = request.Latitude;



                session.Update(container);

                session.Commit();
            }
            catch (Exception ex)
            {
                session.Rollback();
                Log.Error(ex, "container Insert Error");
            }
            finally
            {
                session.CloseTransaction();
            }


            return Ok();
        }
        [HttpDelete("{id}")]
        public ActionResult<Container> Delete(int id)
        {
            Container container = session.Containers.Where(x => x.Id == id).FirstOrDefault();
            if (container == null)
            {
                return NotFound();
            }

            try
            {
                session.BeginTransaction();
                session.Delete(container);
                session.Commit();
            }
            catch (Exception ex)
            {
                session.Rollback();
                Log.Error(ex, "container Insert Error");
            }
            finally
            {
                session.CloseTransaction();
            }

            return Ok();
        }
    }
}


