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
    public class VehicleController  : ControllerBase
    {

        private readonly IMapperSession session;

            public VehicleController(IMapperSession session)
            {

            this.session = session;
        }
        //get containers that own vehicle
        [HttpGet("GetContainersByVehicleId")]
        public List<Container> GetContainersByVehicleId(int id)
        {
            List<Container> result = session.Containers.Where(x => x.VehicleId == id).ToList();
            return result;
        }
        //get cluster of container by given  vehicle id and cluster number 
        [HttpGet("GetClustersByVehicleId")]
        public ActionResult<List<List<Container>>> GetClustersByVehicleId(int id,int n)
        {
            List<Container> containers = session.Containers.Where(x => x.VehicleId == id).ToList();
            List<List<Container>> result = new List<List<Container>>();
            if (containers.Count < n)
            {
                return BadRequest("N must be smaller than containers.");
            }
            int elementsInContainer = containers.Count / n; 
            int excessElements = containers.Count % n;
            int index = 0;
            int clusterIndex = 0;
            for (int i = 0; i < n; i++)
            {
                List<Container> cluster = new List<Container>();
                for (int j = 0; j < elementsInContainer; j++)
                {
                    cluster.Add(containers[index++]);
                }
                result.Add(cluster);
            }
            for (int i = excessElements; i > 0; i--)
            {
                result[clusterIndex++].Add(containers[index++]);
            }
            return result;
        }
        //get all vehicles
        // GET: api/<VehicleController>
        [HttpGet]
            public IEnumerable<Vehicle> Get()
            {
            return session.Vehicles; 
            }
        //get vehicle by given id
            // GET api/<VehicleController>/5
            [HttpGet("{id}")]
            public Vehicle Get(int id)
            {
                return session.Vehicles.Where(x => x.Id == id).FirstOrDefault();
        }
        //create new vehicle
            // POST api/<VehicleController>
            [HttpPost]
            public void Post([FromBody] Vehicle vehicle)
            {
            try
            {
                session.BeginTransaction();
                session.Save(vehicle);
                session.Commit();
            }
            catch (Exception ex)
            {
                session.Rollback();
                Log.Error(ex, "vehicles Insert Error");
            }
            finally
            {
                session.CloseTransaction();
            }
        }
        //update existing vehicle
            // PUT api/<VehicleController>/5
            [HttpPut]
        public ActionResult<Vehicle> Put([FromBody] Vehicle request)
        {
            Vehicle vehicle = session.Vehicles.Where(x => x.Id == request.Id).FirstOrDefault();
            if (vehicle == null)
            {
                return NotFound();
            }

            try
            {
                session.BeginTransaction();

                vehicle.VehicleName = request.VehicleName;
                vehicle.VehiclePlate = request.VehiclePlate;
               

                session.Update(vehicle);

                session.Commit();
            }
            catch (Exception ex)
            {
                session.Rollback();
                Log.Error(ex, "vehicle Insert Error");
            }
            finally
            {
                session.CloseTransaction();
            }


            return Ok();
        }
        //delete existing  vehicle
        [HttpDelete("{id}")]
        public ActionResult<Vehicle> Delete(int id)
        {
            Vehicle vehicle = session.Vehicles.Where(x => x.Id == id).FirstOrDefault();
            if (vehicle == null)
            {
                return NotFound();
            }

            try
            {
                session.BeginTransaction();
                session.Delete(vehicle);
                session.Commit();
            }
            catch (Exception ex)
            {
                session.Rollback();
                Log.Error(ex, "vehicle Insert Error");
            }
            finally
            {
                session.CloseTransaction();
            }

            return Ok();
        }
    }
    }


