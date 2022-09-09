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

        private readonly IMapperSession<Vehicle> sessionVehicle;
        private readonly IMapperSession<Container> sessionContainer;


        public VehicleController(IMapperSession<Vehicle> sessionVehicle, IMapperSession<Container> sessionContainer)
            {

            this.sessionVehicle = sessionVehicle;
            this.sessionContainer = sessionContainer;
        }
        //get containers that own vehicle
        [HttpGet("GetContainersByVehicleId")]
        public List<Container> GetContainersByVehicleId(int id)
        {
            List<Container> result = sessionContainer.Entities.Where(x => x.VehicleId == id).ToList();
           
            return result;
        }
        //get cluster of container by given  vehicle id and cluster number 
        [HttpGet("GetClustersByVehicleId")]
        public ActionResult<List<List<Container>>> GetClustersByVehicleId(int id,int n)
        {
            List<Container> containers = sessionContainer.Entities.Where(x => x.VehicleId == id).ToList();
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
            return sessionVehicle.Entities; 
            }
        //get vehicle by given id
            // GET api/<VehicleController>/5
            [HttpGet("{id}")]
            public Vehicle Get(int id)
            {
                return sessionVehicle.Entities.Where(x => x.Id == id).FirstOrDefault();
        }
        //create new vehicle
            // POST api/<VehicleController>
            [HttpPost]
            public void Post([FromBody] Vehicle vehicle)
            {
            try
            {
                sessionVehicle.BeginTransaction();
                sessionVehicle.Save(vehicle);
                sessionVehicle.Commit();
            }
            catch (Exception ex)
            {
                sessionVehicle.Rollback();
                Log.Error(ex, "vehicles Insert Error");
            }
            finally
            {
                sessionVehicle.CloseTransaction();
            }
        }
        //update existing vehicle
            // PUT api/<VehicleController>/5
            [HttpPut]
        public ActionResult<Vehicle> Put([FromBody] Vehicle request)
        {
            Vehicle vehicle = sessionVehicle.Entities.Where(x => x.Id == request.Id).FirstOrDefault();
            if (vehicle == null)
            {
                return NotFound();
            }

            try
            {
                sessionVehicle.BeginTransaction();

                vehicle.VehicleName = request.VehicleName;
                vehicle.VehiclePlate = request.VehiclePlate;


                sessionVehicle.Update(vehicle);

                sessionVehicle.Commit();
            }
            catch (Exception ex)
            {
                sessionVehicle.Rollback();
                Log.Error(ex, "vehicle Insert Error");
            }
            finally
            {
                sessionVehicle.CloseTransaction();
            }


            return Ok();
        }
        //delete existing  vehicle
        [HttpDelete("{id}")]
        public ActionResult<Vehicle> Delete(int id)
        {
            Vehicle vehicle = sessionVehicle.Entities.Where(x => x.Id == id).FirstOrDefault();
            if (vehicle == null)
            {
                return NotFound();
            }

            try
            {
                sessionVehicle.BeginTransaction();
                sessionVehicle.Delete(vehicle);
                sessionVehicle.Commit();
            }
            catch (Exception ex)
            {
                sessionVehicle.Rollback();
                Log.Error(ex, "vehicle Insert Error");
            }
            finally
            {
                sessionVehicle.CloseTransaction();
            }

            return Ok();
        }
    }
    }


