using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ManBTarjetas.Models;
using System.IO;
using System.Collections;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ManBTarjetas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarjetaController : ControllerBase
    {

        private List<TarjetaModel> tarjetas = new List<TarjetaModel>();

        // GET: api/<TarjetaController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                string[] files = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\tarjetas");

                foreach (string file in files) {

                    if (System.IO.File.Exists(file)) {

                        string text = System.IO.File.ReadAllText(file);

                        string[] lines = text.Split("\n");

                        TarjetaModel tm = new TarjetaModel();

                        tm.id = Convert.ToInt32(lines[0].Substring(lines[0].IndexOf(":") + 2, (lines[0].Length - lines[0].IndexOf(":")) - 2));
                        tm.titular = lines[1].Substring(lines[1].IndexOf(":") + 2, (lines[1].Length - lines[1].IndexOf(":") -2));
                        tm.numeroTarjeta = lines[2].Substring(lines[2].IndexOf(":") + 2, (lines[2].Length - lines[2].IndexOf(":") -2));
                        tm.fechaExpiracion = lines[3].Substring(lines[3].IndexOf(":") + 2, (lines[3].Length - lines[3].IndexOf(":") -2));
                        tm.cvv = lines[4].Substring(lines[4].IndexOf(":") + 2, (lines[4].Length - lines[4].IndexOf(":") -3));

                        tarjetas.Add(tm);

                    }
                
                }

                return Ok(tarjetas);
            }
            catch (Exception e) {

                return BadRequest(e.Message);
            
            }
            
        }

        // POST api/<TarjetaController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TarjetaModel tarjeta)
        {
            try
            {
                Random rd = new Random();
                tarjeta.id = rd.Next(0, 1000000);


                using (StreamWriter file = new StreamWriter(Directory.GetCurrentDirectory() + "\\Tarjetas\\" + tarjeta.id + ".txt"))
                {
                    
                    file.WriteLine("id: " + tarjeta.id + "\ntitular: " + tarjeta.titular + "\nnumeroTarjeta: " + tarjeta.numeroTarjeta + "\nfechaExpiracion: " + tarjeta.fechaExpiracion + "\ncvv: " +tarjeta.cvv);
                       
                }

                return Ok(tarjeta);

            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

        }

        // PUT api/<TarjetaController>/5
        [HttpPut]
        public async Task<IActionResult> Put(string numeroTarjeta, [FromBody] TarjetaModel tarjeta)
        {

            try
            {

                TarjetaModel tm = new TarjetaModel();
                string[] files = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\Tarjetas");

                foreach (string file in files)
                {

                    if (System.IO.File.Exists(file))
                    {

                        string text = System.IO.File.ReadAllText(file);

                        string[] lines = text.Split("\n");


                        if ((lines[2].Substring(lines[2].IndexOf(":") + 2, (lines[2].Length - lines[2].IndexOf(":") - 2))).Equals(numeroTarjeta))
                        {
                            tarjeta.id = Convert.ToInt32(lines[0].Substring(lines[0].IndexOf(":") + 2, (lines[0].Length - lines[0].IndexOf(":")) - 2));
                            

                            System.IO.File.Delete(@file);

                            using (StreamWriter f = new StreamWriter(Directory.GetCurrentDirectory() + "\\Tarjetas\\" + tarjeta.id + ".txt"))
                            {

                                f.WriteLine("id: " + tarjeta.id + "\ntitular: " + tarjeta.titular + "\nnumeroTarjeta: " + tarjeta.numeroTarjeta + "\nfechaExpiracion: " + tarjeta.fechaExpiracion + "\ncvv: " + tarjeta.cvv);

                            }

                        }




                        tarjetas.Add(tarjeta);

                    }

                }

                return Ok(tarjeta);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        // DELETE api/<TarjetaController>/5
        [HttpDelete]
        public async Task<IActionResult> Delete(string numeroTarjeta)
        {

            TarjetaModel tm = new TarjetaModel();

            try
            {

                string[] files = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\Tarjetas");

                foreach (string file in files)
                {

                    if (System.IO.File.Exists(file))
                    {

                        string text = System.IO.File.ReadAllText(file);

                        string[] lines = text.Split("\n");


                        if ((lines[2].Substring(lines[2].IndexOf(":") + 2, (lines[2].Length - lines[2].IndexOf(":") - 2))).Equals(numeroTarjeta)) {

                            tm.id = Convert.ToInt32(lines[0].Substring(lines[0].IndexOf(":") + 2, (lines[0].Length - lines[0].IndexOf(":")) - 2));
                            tm.titular = lines[1].Substring(lines[1].IndexOf(":") + 2, (lines[1].Length - lines[1].IndexOf(":") - 2));
                            tm.numeroTarjeta = lines[2].Substring(lines[2].IndexOf(":") + 2, (lines[2].Length - lines[2].IndexOf(":") - 2));
                            tm.fechaExpiracion = lines[3].Substring(lines[3].IndexOf(":") + 2, (lines[3].Length - lines[3].IndexOf(":") - 2));
                            tm.cvv = lines[4].Substring(lines[4].IndexOf(":") + 2, (lines[4].Length - lines[4].IndexOf(":") - 3));
                            System.IO.File.Delete(@file);
                        
                        }


                        

                        tarjetas.Add(tm);

                    }

                }

                return Ok(tm);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

