﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Models;

public class Note
{
    public string Filename { get; set; }
    public string Text { get; set; }
    public DateTime Date => File.Exists(Filename) ? File.GetCreationTime(Filename) : DateTime.Now;
}