﻿namespace Backend.Api.Models
{
    public class RolPermiso
    {
        public int RolId { get; set; }
        public Rol Rol { get; set; }
        public int PermisoId { get; set; }
        public Permiso Permiso { get; set; }
    }
}
