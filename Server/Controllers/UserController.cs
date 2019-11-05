﻿using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Server.Models.GunFactory;
using System.Diagnostics;

namespace Server.Controllers
{
    [RoutePrefix("api/users")]
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "X-My-Header")]
    public class UserController : ApiController
    {
        [HttpGet]
        public Weapon GetPistol()
        {
            //Weapon weapon = new PistolFactory().CreateWeapon();

            IWeapon weapon = new Weapon();
            Debug.WriteLine(weapon.getAmmo());
            weapon = new ExtraAmmoDecorator(weapon);
            Debug.WriteLine(weapon.getAmmo());
            return new Weapon();
        }
    }
}
