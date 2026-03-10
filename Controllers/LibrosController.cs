using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.SqlClient;

namespace Biblioteca.Controllers
{
    public class LibrosController : Controller
    {
        string cadenaConexion = "Server=.;Database=Biblioteca;Trusted_Connection=True;TrustServerCertificate=True";
        public IActionResult Index()
        {
            List<Libro> lista = new List<Libro>();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                string query = @"SELECT L.Id, L.Titulo, L.Publicacion,
                                A.Nombre AS Autor,
                                C.Nombre AS Categoria
                                FROM Libros L
                                INNER JOIN Autores A ON L.IdAutor = A.Id
                                INNER JOIN Categorias C ON L.IdCategoria = C.Id";

                SqlCommand cmd = new SqlCommand(query, conexion);

                conexion.Open();

                SqlDataReader lector = cmd.ExecuteReader();

                while (lector.Read())
                {
                    Libro libro = new Libro();

                    libro.Id = Convert.ToInt32(lector["Id"]);
                    libro.Titulo = lector["Titulo"].ToString();
                    libro.Publicacion = Convert.ToInt32(lector["Publicacion"]);
                    libro.Autor = lector["Autor"].ToString();
                    libro.Categoria = lector["Categoria"].ToString();

                    lista.Add(libro);
                }
            }

            return View(lista);
        }
        public IActionResult Create()
        {
            ViewBag.Autores = ObtenerAutores();
            ViewBag.Categorias = ObtenerCategorias();

            return View();
        }
        [HttpPost]
        public IActionResult Create(Libro libro)
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                string query = @"INSERT INTO Libros (Titulo, Publicacion, IdAutor, IdCategoria)
                                 VALUES (@titulo, @publicacion, @autor, @categoria)";

                SqlCommand cmd = new SqlCommand(query, conexion);

                cmd.Parameters.AddWithValue("@titulo", libro.Titulo);
                cmd.Parameters.AddWithValue("@publicacion", libro.Publicacion);
                cmd.Parameters.AddWithValue("@autor", libro.IdAutor);
                cmd.Parameters.AddWithValue("@categoria", libro.IdCategoria);

                conexion.Open();
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }
        private List<SelectListItem> ObtenerAutores()
        {
            List<SelectListItem> lista = new List<SelectListItem>();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                string query = "SELECT Id, Nombre FROM Autores";

                SqlCommand cmd = new SqlCommand(query, conexion);

                conexion.Open();

                SqlDataReader lector = cmd.ExecuteReader();

                while (lector.Read())
                {
                    lista.Add(new SelectListItem
                    {
                        Value = lector["Id"].ToString(),
                        Text = lector["Nombre"].ToString()
                    });
                }
            }

            return lista;
        }
        private List<SelectListItem> ObtenerCategorias()
        {
            List<SelectListItem> lista = new List<SelectListItem>();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                string query = "SELECT Id, Nombre FROM Categorias";

                SqlCommand cmd = new SqlCommand(query, conexion);

                conexion.Open();

                SqlDataReader lector = cmd.ExecuteReader();

                while (lector.Read())
                {
                    lista.Add(new SelectListItem
                    {
                        Value = lector["Id"].ToString(),
                        Text = lector["Nombre"].ToString()
                    });
                }
            }

            return lista;
        }
    }
}