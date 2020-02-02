using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test_Iqvia.Models;

namespace Test_Iqvia.Controllers
{
    [Route("files")]
    [ApiController]
    public class FileUploadsController : ControllerBase
    {
        private readonly TestDBContext _context;
        public static IWebHostEnvironment _enviroment { get; set; }

        public FileUploadsController(TestDBContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _enviroment = environment;
        }

        // GET: api/FileUploads
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FileUpload>>> GetFiles()
        {
            return await _context.Files.ToListAsync();
        }

        // GET: api/FileUploads/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FileUpload>> GetFileUpload(int id)
        {
            var fileUpload = await _context.Files.FindAsync(id);

            if (fileUpload == null)
            {
                return NotFound();
            }

            return fileUpload;
        }



        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<FileUpload>>> GetFileUpload([FromQuery]string titulo = "",[FromQuery] string diretor = "", [FromQuery] string elenco = "", [FromQuery] int anoInicio = 1950, [FromQuery] int anoFim = 2100)
        {
            //return await _context.Files.Where(item => 
            //    (
            //     item.Titulo.ToLower().Contains(titulo.ToLower()) ||
            //     item.Direcao.ToLower().Contains(diretor.ToLower()) ||
            //     item.Elenco.ToLower().Contains(elenco.ToLower())
            //    ) 
            //    &&
            //    (item.Ano >= anoInicio && item.Ano <= anoFim)
            //    ).ToListAsync();
            List<FileUpload> retorno = new List<FileUpload>();

            retorno = await _context.Files.ToListAsync();

            if (!string.IsNullOrWhiteSpace(titulo))
            {
                retorno = retorno.Where(item => item.Titulo.ToLower().Contains(titulo.ToLower())).ToList();
            }

            if (!string.IsNullOrWhiteSpace(diretor))
            {
                retorno = retorno.Where(item => item.Direcao.ToLower().Contains(diretor.ToLower())).ToList();
            }

            if (!string.IsNullOrWhiteSpace(elenco))
            {
                retorno = retorno.Where(item => item.Elenco.ToLower().Contains(elenco.ToLower())).ToList();
            }


            return retorno.Where(item => item.Ano >= anoInicio && item.Ano <= anoFim).ToList();
        }

        // PUT: api/FileUploads/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFileUpload(int id, FileUpload fileUpload)
        {
            if (id != fileUpload.Id)
            {
                return BadRequest();
            }

            _context.Entry(fileUpload).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FileUploadExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/FileUploads
        [HttpPost]
        //public async Task<ActionResult<FileUpload>> PostFileUpload(FileUpload fileUpload,[FromBody] FileUploadAPI objFile)
        public async Task<ActionResult<FileUpload>> PostFileUpload([FromForm] FIleSender file)
        {
            VerifyFile(file.files);

            var arquivo = new FileUpload()
            {
                Titulo = file.Titulo,
                Ano = file.Ano,
                Direcao = file.Direcao,
                Duracao = file.Duracao,
                Elenco = file.Elenco,
                Sinopse = file.Sinopse,
                TipodeMidia = file.TipodeMidia,
                NomeArquivo = file.files.FileName
            };

            _context.Files.Add(arquivo);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFileUpload", new { id = arquivo.Id }, arquivo);
        }

        [HttpPost]
        [Route("/files/update/{id}")]
        public async Task<ActionResult<FileUpload>> PostUpdateFileUpload([FromForm]FIleSender file, int id)
        {
            var updateFile = _context.Files.First(item => item.Id == id);

            updateFile.Titulo = file.Titulo;
            updateFile.Elenco = file.Elenco;
            updateFile.Direcao = file.Direcao;
            updateFile.TipodeMidia = file.TipodeMidia;
            updateFile.Ano = file.Ano;

            _context.Entry(updateFile).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            _context.SaveChanges();

            return updateFile;
        }

        // DELETE: api/FileUploads/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<FileUpload>> DeleteFileUpload(int id)
        {
            var fileUpload = await _context.Files.FindAsync(id);
            if (fileUpload == null)
            {
                return NotFound();
            }

            _context.Files.Remove(fileUpload);
            await _context.SaveChangesAsync();

            return fileUpload;
        }

        private bool FileUploadExists(int id)
        {
            return _context.Files.Any(e => e.Id == id);
        }

        private void VerifyFile(IFormFile files)
        {
            var path = $"{_enviroment.WebRootPath}\\Uploads\\";

            if (files != null)
            {
                if (files.Length > 0)
                {
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    using (FileStream fs = System.IO.File.Create(path + files.FileName))
                    {
                        files.CopyTo(fs);
                        fs.Flush();
                    }
                }
            }           
        }
    }
}
