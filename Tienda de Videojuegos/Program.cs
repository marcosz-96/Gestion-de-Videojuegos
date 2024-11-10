using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static Tienda_de_Videojuegos.Program;

namespace Tienda_de_Videojuegos
{
    public struct Videojuegos
    {
        public string Nombre;
        public char Categoria;
        public double Precio;
        public int EdadMinima;
        public bool Disponibilidad;

        public Videojuegos(string nombre, char categoria, double precio, int edadminima, bool disponibilidad)
        {
            Nombre = nombre;
            Categoria = categoria;
            Precio = precio;
            EdadMinima = edadminima;
            Disponibilidad = disponibilidad;
        }
    }
    internal class Program
    {
        static List<Videojuegos> juegos = new List<Videojuegos>();
        //arreglos se escribe con llaves
        static string[] empresas = { "Empresa 1", "Empresa 2", "Empresa 3" }; //3 empresas
        //matrices con corchetes y , en el medio
        static int[,] ventas = new int[empresas.Length, 12]; //12 meses del año


        static bool existe = false;
        static string palabra, nombre;
        static int opcion, edad, numero, contador;
        static double sumarPrecio;

        static void Main(string[] args)
        {
            do
            {
                MensajeBienvenida();
                opcion = int.Parse(Console.ReadLine());
                Menu();
            } while (opcion != 0);
        }
        static void MensajeBienvenida()
        {
            Console.Clear();
            Console.WriteLine("8===>Bienvenido al programa gestion de Juegos!<===8\n");
            Console.WriteLine("Seleccione una opcion:");
            Console.WriteLine("1: Agregar juegos a la Lista.");
            Console.WriteLine("2: Ver lista de juegos Agregados.");
            Console.WriteLine("3: Ordenar precios de manera Descendente.");
            Console.WriteLine("4: Ordenar precios de manera Ascendente.");
            Console.WriteLine("5: Filtrar juegos por Edad.");
            Console.WriteLine("6: Registrar Ventas.");
            Console.WriteLine("7: Calcular Promedio de Precios en juegos Disponibles");
            Console.WriteLine("0: salir.");
     
        }
        static void Menu()
        {
            Console.Clear();
            switch (opcion)
            {
                case 1:
                    AgregarJuego(juegos, "");
                    break;
                case 2:
                    ListaDeJuegos(juegos);
                    break;
                case 3:
                    OrdenarPrecioDescendente(juegos);
                    break;
                case 4:
                    OdenarPrecioAscendente(juegos);
                    break;
                case 5:
                    FiltrarPorEdad(juegos, edad);
                    break;
                case 6:
                    RegistarVentas(juegos, empresas, ventas, numero);
                    break;
                case 7:
                    CalcularPromedioDePrecio(juegos);
                    break;
                case 0:
                    Console.WriteLine("Fin del programa");
                    break;
                default:
                    Console.WriteLine("Opcion invalida. Ingrese una opcion del menu");
                    break;
            }
            
        }
        static void AgregarJuego(List<Videojuegos> juegos, string palabra)
        {
            Console.Clear();
            bool continuarIngresando = true;
            while (continuarIngresando)
            {
                Console.WriteLine("Para agregar un nuevo juego, ingrese la palabra 'nuevo' o 'menu' para salir");
                palabra = Console.ReadLine();

                if (palabra == "menu")
                {
                    Console.WriteLine("\nHa salido de la opcion agregar juegos");
                    continuarIngresando = false;
                }
                else if (palabra == "nuevo")
                {
                    Console.Write("\nIngrese el nombre del juego: ");
                    string nombre = Console.ReadLine();
                    //verificacion de nombre exitente
                    foreach (var juego in juegos)
                    {
                        if (juego.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine("\nEl nombre ya existe. Ingrese otro nombre");
                            return;
                        }
                    }

                    Console.Write("\nIngrese la categoria del juego ('A' - accion, 'R' - rol, 'D' - deporte): ");
                    char categoria = char.ToUpper(Console.ReadLine()[0]);

                    //validamos la categoria
                    if (!ValidarCategoria(categoria))
                    {
                        Console.WriteLine("Categoria invalida. Ingrese 'A', 'R' o 'D'");
                        return;
                    }
                    else if (categoria == 'A')
                    {
                        Console.WriteLine("Es un juego de Accion.");
                    }
                    else if (categoria == 'R')
                    {
                        Console.WriteLine("Es un juego de Rol.");
                    }
                    else if (categoria == 'D')
                    {
                        Console.WriteLine("Es un juego de Deportes.");
                    }

                    Console.Write("\nIngrese el precio del juego: ");
                    double precio;
                    //validacion de precio
                    while (!double.TryParse(Console.ReadLine(), out precio) || precio < 0)
                    {
                        Console.WriteLine("\nValor invalido. Ingrese un valor correcto");
                    }

                    Console.Write("\nIngrese la edad minima para jugar: ");
                    int edadMinima;
                    //validamos edad
                    while (!int.TryParse(Console.ReadLine(), out edadMinima) || edadMinima < 0)
                    {
                        Console.WriteLine("Edad invalida. Ingrese un numero positivo");
                    }

                    Console.Write("\nEl juego se encuentra disponible? (true/false): ");
                    //string disponibilidadInput = Console.ReadLine();
                    bool disponibilidad; //= disponibilidadInput.Equals(" ", StringComparison.OrdinalIgnoreCase);

                    while (!bool.TryParse(Console.ReadLine(), out disponibilidad))
                    {
                        Console.WriteLine("Dato invalido. Ingrese 'true' o 'false'");
                    }
                    //una vez verificado los datos ingresamos el juego a la lista

                    Videojuegos nuevoJuego = new Videojuegos(nombre, categoria, precio, edadMinima, disponibilidad);
                    juegos.Add(nuevoJuego);

                    Console.WriteLine("\nEl juego se ingresó exitosamente!");
                    Console.WriteLine();
                    
                }
            }
        }
        static bool ValidarCategoria(char categoria)
        {
            return categoria == 'A' || categoria == 'R' || categoria == 'D';
        }
        //funcion opcional, simplificada...
        static void IngresarJuego()
        {
            Videojuegos nuevo;
            bool existe = false;

            Console.WriteLine("Ingrese el nombre del juego: ");
            nuevo.Nombre = Console.ReadLine();

            foreach (var datos in juegos)
            {
                if (datos.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase))
                {
                    existe = true;
                    break;
                }
            }
            if (existe)
            {
                Console.WriteLine("El nombre ya existe. Ingrese otro nombre");
                return;
            }
            Console.WriteLine("Ingrese la categoria (A/R/D): ");
            nuevo.Categoria = char.ToUpper(Console.ReadKey().KeyChar);
            Console.WriteLine("Ingrese el precio: ");
            nuevo.Precio = double.Parse(Console.ReadLine());
            Console.WriteLine("Ingrese la edad minima recomendada: ");
            nuevo.EdadMinima = int.Parse(Console.ReadLine());
            Console.WriteLine("Ingrese su disponibilidad (true/false): ");
            nuevo.Disponibilidad = bool.Parse(Console.ReadLine());

            juegos.Add(nuevo);
            Console.WriteLine("Juego agregado exitosamente!");
            Console.Clear();
        }

        static void ListaDeJuegos(List<Videojuegos> juegos)
        {
            Console.Clear();
            juegos.Add(new Videojuegos
            {
                Nombre = "Aventuras Épicas",
                Categoria = 'A',
                Precio = 49.99,
                EdadMinima = 10,
                Disponibilidad = true
            });
            juegos.Add(new Videojuegos
            {
                Nombre = "RoboWars",
                Categoria = 'R',
                Precio = 59.99,
                EdadMinima = 12,
                Disponibilidad = false
            });
            juegos.Add(new Videojuegos
            {
                Nombre = "Puzzle Mania",
                Categoria = 'D',
                Precio = 19.99,
                EdadMinima = 5,
                Disponibilidad = true
            });
            juegos.Add(new Videojuegos
            {
                Nombre = "Simulador de Vida",
                Categoria = 'A',
                Precio = 39.99,
                EdadMinima = 16,
                Disponibilidad = true
            });
            juegos.Add(new Videojuegos
            {
                Nombre = "Carreras Extrema",
                Categoria = 'R',
                Precio = 29.99,
                EdadMinima = 7,
                Disponibilidad = true
            });

            Console.WriteLine("\nLa lista de juegos es: ");
            
            foreach (var datos in juegos)
            {
                Console.WriteLine($"Nombre: {datos.Nombre}, Categoria: {datos.Categoria}, Precio: {datos.Precio}, " +
                    $"Edad Minima: {datos.EdadMinima}, Disponibilidad: {datos.Disponibilidad}");
            }
            Console.WriteLine("\nPresione 'enter' para volver al menu.");
            Console.ReadKey();
            
        }
        static void OrdenarPrecioDescendente(List<Videojuegos> juegos)
        {
            Console.Clear();
            Console.WriteLine("La lista de juegos sin ordenar: ");

            foreach (var datos in juegos)
            {
                Console.WriteLine($"Nombre: {datos.Nombre}, Categoria: {datos.Categoria}, Precio: {datos.Precio}, " +
                    $"Edad Minima: {datos.EdadMinima}, Disponibilidad: {datos.Disponibilidad}");
            }

            //ordenamos de forma ascendente:
            for (int i = 0; i < juegos.Count; i++)
            {
                for (int j = 0; j < juegos.Count -1 -i; j++) 
                {
                    //en este caso ordenamos por Precio
                    if (juegos[j].Precio < juegos[j + 1].Precio)
                    {
                        var temp = juegos[j];
                        juegos[j] = juegos[j + 1];
                        juegos[j + 1] = temp;
                    }
                }

            }
            Console.WriteLine("\nLa lista de juegos ordenada de forma DESCENDENTE con bubble sort: ");

            foreach (var datos in juegos)
            {
                Console.WriteLine($"Nombre: {datos.Nombre}, Categoria: {datos.Categoria}, Precio: {datos.Precio}, " +
                    $"Edad Minima: {datos.EdadMinima}, Disponibilidad: {datos.Disponibilidad}");
            }
            Console.WriteLine("\nPresione 'enter' para volver al menu");
            Console.ReadKey();
        }
        static void OdenarPrecioAscendente(List<Videojuegos> juegos)
        {
            Console.Clear();
            Console.WriteLine("\nLa lista de juegos sin ordenar: ");

            foreach (var datos in juegos)
            {
                Console.WriteLine($"Nombre: {datos.Nombre}, Categoria: {datos.Categoria}, Precio: {datos.Precio}, " +
                    $"Edad Minima: {datos.EdadMinima}, Disponibilidad: {datos.Disponibilidad}");
            }

            //Ordenamos de forma Descendente
            for (int i = 0;i < juegos.Count -1; i++)
            {
                for(int j = 0;j < juegos.Count -1 -i; j++)
                {
                    if (juegos[j].Precio > juegos[j +1].Precio)
                    {
                        var temp = juegos[j];
                        juegos[j] = juegos [j + 1];
                        juegos[j + 1] = temp;
                    }
                }
            }
            Console.WriteLine("\nLa lista de juegos ordenada de forma ASCENDENTE por bubble sort: ");

            foreach (var datos in juegos)
            {
                Console.WriteLine($"Nombre: {datos.Nombre}, Categoria: {datos.Categoria}, Precio: {datos.Precio}, " +
                    $"Edad Minima: {datos.EdadMinima}, Disponibilidad: {datos.Disponibilidad}");
            }
            Console.WriteLine("\nPresione 'enter' para volver al menu");
            Console.ReadKey();
        }
        static void FiltrarPorEdad(List<Videojuegos> juegos, int edad)
        {
            Console.Clear();
            Console.Write("Ingrese una edad: ");
            edad = int.Parse(Console.ReadLine());

            Console.WriteLine("\n--->Juegos Filtrados por Edad<---");

            foreach (var juegoEdad in juegos)
            {
                if (juegoEdad.EdadMinima <= edad)
                {
                    Console.WriteLine($"\nNombre: {juegoEdad.Nombre}, Edad Minima: {juegoEdad.EdadMinima}");
                }
            }
            Console.WriteLine("\nPresione 'enter' para volver al menu");
            Console.ReadKey();
        }
        static void RegistarVentas(List<Videojuegos> juegos, string[] empresas, int[,] ventas, int numero)
        {
            Console.Clear();
            //Mostramos las empresas con las que trabajamos
            Console.WriteLine(">>>Registrando Ventas<<<");
            for (int i = 0; i < empresas.Length; i++)
            {
                Console.WriteLine($"{i + 1} {empresas[i]}");
            }
            //elegios una empresa para registrar ventas
            Console.Write("Seleccione una Empresa entre (1-3): ");
            numero = int.Parse(Console.ReadLine()) -1;
            //verificamos que el indice sea válido
            if (numero < 0 || numero > empresas.Length) 
            {
                Console.WriteLine("Empresa no válida. Ingrese un número válido");
            }
            //registramos ventas para cada mes
            for (int mes = 0; mes < 12; mes++) 
            {
                Console.WriteLine($"Ingrese la cantidad de juegos vendido en {mes +1} mes: ");
                ventas[numero, mes] = int.Parse(Console.ReadLine());
            }
            Console.WriteLine("\nVentas registradas exitosamnete!");
            Console.WriteLine("Presione 'enter' para volver al menú");
            Console.ReadKey();
        }
        static void CalcularPromedioDePrecio(List<Videojuegos> juegos)
        {
            Console.Clear();
            sumarPrecio = 0;
            contador = 0;

            //calcular la suma de los precios de los juegos disponibles
            foreach (var disponible in juegos)
            {
                if (disponible.Disponibilidad)
                {
                    sumarPrecio += disponible.Precio;
                    contador++;
                }
            }
            //mostrar el promedio de los juegos disponibles
            if (contador > 0) 
            {
                double promedio = sumarPrecio / contador;
                Console.WriteLine($"\nEl promedio de precios en los juegos disponibles es: {promedio}");
            }
            else
            {
                Console.WriteLine("\nNo hay juegos disponibles para calcular el promedio.");
            }
            Console.WriteLine("Presione 'enter' para volver al menú");
            Console.ReadKey();
        }
    }
}
