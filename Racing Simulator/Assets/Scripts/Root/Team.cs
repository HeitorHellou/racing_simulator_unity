﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
  public class Team
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string LogoString { get; set; }
    public string CarString { get; set; }
    public int Score { get; set; }
    public float LapTime { get; set; }
    public int points { get; set; }
    public int pneu_id { get; set; }
    public int pneu_dura { get; set; }
    public Car Car { get; set; }
    public Pilot Pilot { get; set; }

    public Team(int id, string name, string logoName, string carName, int pilotId)
    {
      Id = id;
      Name = name;
      LogoString = logoName;
      CarString = carName;
      Pilot = World.GetPilotById(pilotId);
    }

    public void SetPilot(int id)
    {
      Pilot = World.GetPilotById(id);
    }

    public void SetScore(int score)
    {
      Score = score;
    }

    public void SetLapTime(float time)
    {
      LapTime = time;
    }
  }
}
