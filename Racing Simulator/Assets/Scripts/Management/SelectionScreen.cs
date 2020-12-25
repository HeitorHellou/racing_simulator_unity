﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using TMPro;

public class SelectionScreen : MonoBehaviour
{
  // Pilot 1 Texts
  public TextMeshProUGUI namePilot1; 
  public TextMeshProUGUI countryPilot1;
  public TextMeshProUGUI agePilot1;
  public TextMeshProUGUI overPilot1;
  public GameObject pilot1;

  // Pilot 2 Texts
  public TextMeshProUGUI namePilot2;
  public TextMeshProUGUI countryPilot2;
  public TextMeshProUGUI agePilot2;
  public TextMeshProUGUI overPilot2;
  public GameObject pilot2;

  // Pilot 3 Texts
  public TextMeshProUGUI namePilot3;
  public TextMeshProUGUI countryPilot3;
  public TextMeshProUGUI agePilot3;
  public TextMeshProUGUI overPilot3;
  public GameObject pilot3;

  public GameObject arrow;
  public string teamName;

  // Integer for pilot selection
  int selection = 1;

  private void Start()
  {
    // Getting the info from World.cs
    namePilot1.text = World.pilots[0].Name; 
    countryPilot1.text = World.pilots[0].Country;
    agePilot1.text = World.pilots[0].Age.ToString() + " Years";
    overPilot1.text = World.pilots[0].Over.ToString() + " Over";
    pilot1.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Pilots/" + World.pilots[0].PilotString);

    namePilot2.text = World.pilots[1].Name;
    countryPilot2.text = World.pilots[1].Country;
    agePilot2.text = World.pilots[1].Age.ToString() + " Years";
    overPilot2.text = World.pilots[1].Over.ToString() + " Over";
    pilot2.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Pilots/" + World.pilots[1].PilotString);

    namePilot3.text = World.pilots[2].Name;
    countryPilot3.text = World.pilots[2].Country;
    agePilot3.text = World.pilots[2].Age.ToString() + " Years";
    overPilot3.text = World.pilots[2].Over.ToString() + " Over";
    pilot3.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Pilots/" + World.pilots[2].PilotString);
  }

  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.LeftArrow))
    {
      arrow.transform.position = new Vector2(arrow.transform.position.x - 5f, arrow.transform.position.y);
      CheckBoundaries();
      selection--;
      if (selection < 0)
        selection = 0;
    }

    if (Input.GetKeyDown(KeyCode.RightArrow))
    {
      arrow.transform.position = new Vector2(arrow.transform.position.x + 5f, arrow.transform.position.y);
      CheckBoundaries();
      selection++;
      if (selection > 2)
        selection = 2;
    }

    if (Input.GetKeyDown(KeyCode.Return))
    {
      // Setting the player chosen pilot on game session
      FindObjectOfType<GameSession>().SetPlayerPilot(selection);
      FindObjectOfType<SceneLoader>().LoadScene(1);
    }
  }

  // Checking the boundaries in which the arrow pointer can move
  public void CheckBoundaries()
  {
    if (arrow.transform.position.x > 5f)
    {
      arrow.transform.position = new Vector2(5f, 2f);
    }
    if (arrow.transform.position.x < -5f)
    {
      arrow.transform.position = new Vector2(-5f, 2f);
    }
  }
}