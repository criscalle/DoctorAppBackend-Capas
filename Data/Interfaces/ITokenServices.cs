﻿using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces;

public interface ITokenServices
{
    Task<string> CreateToken(UserApplication user);
    
}
