using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Assets.Scripts;

public class Race : MonoBehaviour
{
  // Set Qualification and Race Screen
  public GameObject quali_screen;
  public GameObject race_screen;

  // Management of Qualification Texts
  public TextMeshProUGUI first_place;
  public TextMeshProUGUI second_place;
  public TextMeshProUGUI third_place;
  public TextMeshProUGUI fourth_place;
  public TextMeshProUGUI fifth_place;

  // Management Grand Prix Name and lap Text 
  public TextMeshProUGUI gp_name;
  public TextMeshProUGUI gp_lap;

  // Management of Race Position Texts
  public TextMeshProUGUI first_race;
  public TextMeshProUGUI second_race;
  public TextMeshProUGUI third_race;
  public TextMeshProUGUI fourth_race;
  public TextMeshProUGUI fifth_race;
  public TextMeshProUGUI sixth_race;
  public TextMeshProUGUI seventh_race;
  public TextMeshProUGUI eighth_race;
  public TextMeshProUGUI nineth_race;
  public TextMeshProUGUI tenth_race;

  // Management of Race Time Texts
  //public TextMeshProUGUI first_time;
  public TextMeshProUGUI second_time;
  public TextMeshProUGUI third_time;
  public TextMeshProUGUI fourth_time;
  public TextMeshProUGUI fifth_time;
  public TextMeshProUGUI sixth_time;
  public TextMeshProUGUI seventh_time;
  public TextMeshProUGUI eighth_time;
  public TextMeshProUGUI nineth_time;
  public TextMeshProUGUI tenth_time;

  // Tyres Text
  public TextMeshProUGUI first_tyre;
  public TextMeshProUGUI sec_tyre;
  public TextMeshProUGUI thi_tyre;
  public TextMeshProUGUI four_tyre;
  public TextMeshProUGUI fif_tyre;
  public TextMeshProUGUI six_tyre;
  public TextMeshProUGUI sev_tyre;
  public TextMeshProUGUI eig_tyre;
  public TextMeshProUGUI nin_tyre;
  public TextMeshProUGUI ten_tyre;

  // Setting race sound
  public AudioSource race_sound;

  // Setting back to menu btn
  public GameObject btn_back;

  public int contador = 0;

  public TextMeshProUGUI tempoText;
  public float tempo = 3;

  Track track;
  GameSession session;

  // Creating Leaderboard
  List<Team> leaderboard = new List<Team>();

  private void Start()
  {
    session = FindObjectOfType<GameSession>();

    track = World.tracks[FindObjectOfType<GameSession>().GetCurrentTrack()];

    DefineLeaderboard();

    ShowQuali();
    gp_name.text = track.Name;
    ShowQualiLeaderboard(); 
  }

  // Racing
  public void StartRace()
  {
    race_sound.Play();

    StartCoroutine("Wait");

    //foreach (Team t in leaderboard)
    //{
      //Debug.Log("Piloto: " + t.Pilot.Name + " | Tempo: " + t.LapTime.ToString());
    //}
    
    Debug.Log("Fim de corrida!");
    //btn_back.SetActive(true);
    //GivingPoints();
    FindObjectOfType<GameSession>().NextRace();
  }

  // Reducing each car lap time per lap
  private void ReduceLapTime(Team team)
  {
    if (team.Score >= 30)
    {
      team.SetLapTime(team.LapTime - 1.0f);
    }
    else if (team.Score >= 28)
    {
      team.SetLapTime(team.LapTime - 0.8f);
    }
    else if (team.Score >= 25)
    {
      team.SetLapTime(team.LapTime - 0.5f);
    }
    else if (team.Score >= 20)
    {
      team.SetLapTime(team.LapTime - 0.25f);
    }
    else if (team.Score > 14)
    {
      team.SetLapTime(team.LapTime - 0.20f);
    }
    else if (team.Score <= 14)
    {
      team.SetLapTime(team.LapTime - 0.15f);
    }
  }

  // Defining the correct leaderboard positions
  private void DefineLeaderboard()
  {
    foreach (Team team in World.teams)
    {
      team.SetScore(CalculateTeamScore(team));
      team.pneu_dura = 20;
      leaderboard.Add(team);
    }

    SortLeaderboard();

    float time = 100.00f;
    foreach (Team t in leaderboard)
    {
      t.SetLapTime(time);
      time++;
    }
  }

  // Organizing Positions
  private void SortLeaderboard()
  {
    Team temp;
    for (int i = 0; i < leaderboard.Count; i++)
    {
      for (int j = 0; j < leaderboard.Count - 1; j++)
      {
        if (leaderboard[j + 1].Score > leaderboard[j].Score)
        {
          temp = leaderboard[j];
          leaderboard[j] = leaderboard[j + 1];
          leaderboard[j + 1] = temp;
        }
      }
    }
  }

  // Cars overtaking each other
  private void Overtaking()
  {
    Team temp;
    for (int i = 0; i < leaderboard.Count; i++)
    {
      for (int j = 0; j < leaderboard.Count - 1; j++)
      {
        if (leaderboard[j + 1].LapTime - leaderboard[j].LapTime <= 0.2f)
        {
          temp = leaderboard[j];
          leaderboard[j] = leaderboard[j + 1];
          leaderboard[j + 1] = temp;
        }
      }
    }
  }

  // Creating each team score for Qualification & Race
  public int CalculateTeamScore(Team team)
  {
    int r_power, r_dura, r_aero, r_chass, score = 0;

    r_power = RandomNumberGenerator.NumberBetween(team.Car.Power, track.Power);
    r_dura = RandomNumberGenerator.NumberBetween(team.Car.Durability, track.Durability);
    r_aero = RandomNumberGenerator.NumberBetween(team.Car.Aerodynamics, track.Aerodynamics);
    r_chass = RandomNumberGenerator.NumberBetween(team.Car.Chassis, track.Chassi);

    score = (r_power + r_dura + r_aero + r_chass + session.GetPilotOver() + session.GetPilotTyre()) / 7;

    return score;
  }

  // Showing Qualification elements
  public void ShowQuali()
  {
    race_screen.SetActive(false);
    quali_screen.SetActive(true);
  }

  // Showing Race elements
  public void ShowRace()
  {
    quali_screen.SetActive(false);
    race_screen.SetActive(true);
  }

  // Showing Qualification leaderboard
  public void ShowQualiLeaderboard()
  {
    int contador = 0;
    foreach(Team t in leaderboard)
    {
      if (contador == 0)
        first_place.text = t.Pilot.Name + " | " + t.Name;
      else if (contador == 1)
        second_place.text = t.Pilot.Name + " | " + t.Name;
      else if (contador == 2)
        third_place.text = t.Pilot.Name + " | " + t.Name;
      else if (contador == 3)
        fourth_place.text = t.Pilot.Name + " | " + t.Name;
      else if (contador == 4)
         fifth_place.text = t.Pilot.Name + " | " + t.Name;
      contador++;
    }
  }

  // Showing Race leaderboard
  public void ShowRaceLeaderboard()
  {
    int contador = 0;
    float gapAux = 0.0f;
    foreach (Team t in leaderboard)
    {
      if (contador == 0)
      {
        first_race.text = t.Pilot.Name;
        gapAux = t.LapTime;
      }
      else if (contador == 1)
      {
        second_race.text = t.Pilot.Name;
        second_time.text = "+" + (t.LapTime - gapAux).ToString("N1");
      }
      else if (contador == 2)
      {
        third_race.text = t.Pilot.Name;
        third_time.text = "+" + (t.LapTime - gapAux).ToString("N1");
      }
      else if (contador == 3)
      {
        fourth_race.text = t.Pilot.Name;
        fourth_time.text = "+" + (t.LapTime - gapAux).ToString("N1");
      }
      else if (contador == 4)
      {
        fifth_race.text = t.Pilot.Name;
        fifth_time.text = "+" + (t.LapTime - gapAux).ToString("N1");
      }
      else if (contador == 5)
      {
        sixth_race.text = t.Pilot.Name;
        sixth_time.text = "+" + (t.LapTime - gapAux).ToString("N1");
      }
      else if (contador == 6)
      {
        seventh_race.text = t.Pilot.Name;
        seventh_time.text = "+" + (t.LapTime - gapAux).ToString("N1");
      }
      else if (contador == 7)
      {
        eighth_race.text = t.Pilot.Name; 
        eighth_time.text = "+" + (t.LapTime - gapAux).ToString("N1");
      }
      else if (contador == 8)
      {
        nineth_race.text = t.Pilot.Name;
        nineth_time.text = "+" + (t.LapTime - gapAux).ToString("N1");
      }
      else if (contador == 9)
      {
        tenth_race.text = t.Pilot.Name;
        tenth_time.text = "+" + (t.LapTime - gapAux).ToString("N1");
      }
        
      contador++;
    }
  }

  IEnumerator Wait()
  {
    int laps = track.Laps;
    int current_lap = 0;
    do
    {
      foreach (Team team in leaderboard)
      {
        team.SetScore(CalculateTeamScore(team));

        ReduceLapTime(team);
        ShowRaceLeaderboard();
        team.pneu_dura--;
      }
      Overtaking();
      gp_lap.text = "Lap " + current_lap.ToString() + " / " + laps.ToString();
      current_lap++;
      PitStop();
      SettingTyreText();
      // Wait for X second
      yield return new WaitForSeconds(2f);
    } while (current_lap <= laps);
    btn_back.SetActive(true);
    GivingPoints();
  }

  public void GivingPoints()
  {
    int contador = 0;

    foreach (Team t in leaderboard)
    {
      if(contador == 0)
      {
        t.points += 25;
      }
      else if(contador == 1)
      {
        t.points += 18;
      }
      else if(contador == 2)
      {
        t.points += 15;
      }
      else if(contador == 3)
      {
        t.points += 12;
      }
      else if(contador == 4)
      {
        t.points += 10;
      }
      else if(contador == 5)
      {
        t.points += 8;
      }
      else if(contador == 6)
      {
        t.points += 6;
      }
      else if(contador == 7)
      {
        t.points += 4;
      }
      else if(contador == 8)
      {
        t.points += 2;
      }
      else if(contador == 9)
      {
        t.points += 1;
      }

      contador++;
    }
  }

  public void PitStop()
  {
    //int contador = 0;
    int sort_pneu;

    foreach(Team t in leaderboard)
    {
      if(t.pneu_dura <= 0)
      {
        sort_pneu = RandomNumberGenerator.NumberBetween(1, 3);

        if(sort_pneu == 1)
        {
          Debug.Log("Colocando Pneus Macios");
          t.pneu_id = 30;
          t.pneu_dura = 20;
          t.SetLapTime(t.LapTime + 20.0f);
        }
        else if(sort_pneu == 2)
        {
          Debug.Log("Colocando Pneus M�dios");
          t.pneu_id = 27;
          t.pneu_dura = 30;
          t.SetLapTime(t.LapTime + 20.0f);
        }
        else if(sort_pneu == 3)
        {
          Debug.Log("Colocando Pneus Duros");
          t.pneu_id = 24;
          t.pneu_dura = 40;
          t.SetLapTime(t.LapTime + 20.0f);
        }
      }
    }
  }

  public void StopMusic()
  {
    race_sound.Stop();
  }

  public void SettingTyreText()
  {
    int contador = 0;

    foreach(Team t in leaderboard)
    {
      if(t.pneu_id == 30)
      {
        if(contador == 0)
        {
          first_tyre.text = "S";
        }
        else if (contador == 1)
        {
          sec_tyre.text = "S";
        }
        else if (contador == 2)
        {
          thi_tyre.text = "S";
        }
        else if (contador == 3)
        {
          four_tyre.text = "S";
        }
        else if (contador == 4)
        {
          fif_tyre.text = "S";
        }
        else if (contador == 5)
        {
          six_tyre.text = "S";
        }
        else if (contador == 6)
        {
          sev_tyre.text = "S";
        }
        else if(contador == 7)
        {
          eig_tyre.text = "S";
        }
        else if (contador == 8)
        {
          nin_tyre.text = "S";
        }
        else if (contador == 9)
        {
          ten_tyre.text = "S";
        }
      }
      else if(t.pneu_id == 27)
      {
        if (contador == 0)
        {
          first_tyre.text = "M";
        }
        else if (contador == 1)
        {
          sec_tyre.text = "M";
        }
        else if (contador == 2)
        {
          thi_tyre.text = "M";
        }
        else if (contador == 3)
        {
          four_tyre.text = "M";
        }
        else if (contador == 4)
        {
          fif_tyre.text = "M";
        }
        else if (contador == 5)
        {
          six_tyre.text = "M";
        }
        else if (contador == 6)
        {
          sev_tyre.text = "M";
        }
        else if (contador == 7)
        {
          eig_tyre.text = "M";
        }
        else if (contador == 8)
        {
          nin_tyre.text = "M";
        }
        else if (contador == 9)
        {
          ten_tyre.text = "M";
        }
      }
      else if(t.pneu_id == 24)
      {
        if (contador == 0)
        {
          first_tyre.text = "H";
        }
        else if (contador == 1)
        {
          sec_tyre.text = "H";
        }
        else if (contador == 2)
        {
          thi_tyre.text = "H";
        }
        else if (contador == 3)
        {
          four_tyre.text = "H";
        }
        else if (contador == 4)
        {
          fif_tyre.text = "H";
        }
        else if (contador == 5)
        {
          six_tyre.text = "H";
        }
        else if (contador == 6)
        {
          sev_tyre.text = "H";
        }
        else if (contador == 7)
        {
          eig_tyre.text = "H";
        }
        else if (contador == 8)
        {
          nin_tyre.text = "H";
        }
        else if (contador == 9)
        {
          ten_tyre.text = "H";
        }
      }

      contador++;
    }
  }
}
