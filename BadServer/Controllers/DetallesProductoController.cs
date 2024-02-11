﻿using BadServer.DataBase;
using BadServer.DataBase.Dto;
using BadServer.DataBase.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BadServer.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class DetallesProductoController : ControllerBase
    {
        private readonly MyDbContext _dbContext;

        public DetallesProductoController(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("Producto{id}")]
        public async Task<IActionResult> ObtenerDetalles(int id)
        {
            var producto = await _dbContext.Productos.FindAsync(id);

            if (producto == null)
            {
                return NotFound("Producto no encontrado");
            }

            var detallesProductoDto = new DetallesProductoDto
            {
                ProductoID = producto.ProductoID,
                Cantidad = producto.Cantidad,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                Imagen = producto.Imagen,
                Categoria = producto.Categoria
            };

            return Ok(detallesProductoDto);
        }

        [HttpPost("AñadirProducto")]
        public async Task<IActionResult> AgregarProducto([FromBody] DetallesProductoDto detallesProductoDto)
        { 
            var nuevoProducto = new Producto
            {
                Cantidad = detallesProductoDto.Cantidad,
                Nombre = detallesProductoDto.Nombre,
                Descripcion = detallesProductoDto.Descripcion,
                Precio = detallesProductoDto.Precio,
                Imagen = detallesProductoDto.Imagen,
                Categoria = detallesProductoDto.Categoria
            };

            _dbContext.Productos.Add(nuevoProducto);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(ObtenerDetalles), new { id = nuevoProducto.ProductoID }, detallesProductoDto);
        }

        [HttpPut("EditarProducto{id}")]
        public async Task<IActionResult> EditarProducto(int id, [FromBody] DetallesProductoDto detallesProductoDto)
        {
            if (id != detallesProductoDto.ProductoID)
            {
                return BadRequest("El ID del producto no coincide.");
            }

            var producto = await _dbContext.Productos.FindAsync(id);

            if (producto == null)
            {
                return NotFound("No se ha encontrado el producto");
            }

            producto.Cantidad = detallesProductoDto.Cantidad;
            producto.Nombre = detallesProductoDto.Nombre;
            producto.Descripcion = detallesProductoDto.Descripcion;
            producto.Precio = detallesProductoDto.Precio;
            producto.Imagen = detallesProductoDto.Imagen;
            producto.Categoria = detallesProductoDto.Categoria;

            await _dbContext.SaveChangesAsync();

            return Ok("Producto editado.");
        }

        [HttpDelete("EliminarProducto{id}")]
        public async Task<IActionResult> EliminarProducto(int id)
        {
            var producto = await _dbContext.Productos.FindAsync(id);

            if (producto == null)
            {
                return NotFound("No se ha encontrado el producto.");
            }

            _dbContext.Productos.Remove(producto);
            await _dbContext.SaveChangesAsync();

            return Ok("Producto eliminado.");
        }

    }
}
