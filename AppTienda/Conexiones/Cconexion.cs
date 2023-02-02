using System;
using System.Collections.Generic;
using System.Text;
using Firebase.Database;

namespace AppTienda.Conexiones
{
    public class Cconexion
    {
        // FirebaseClient es la clase que nos permite conectarnos a la base de datos
        public static FirebaseClient conexionFireBase = new FirebaseClient("https://apptiendaxamarin-default-rtdb.firebaseio.com/");
    }
}