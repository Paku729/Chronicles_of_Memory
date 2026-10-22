using System;
using System.Collections.Generic;

[Serializable]
public class StoryData
{
    public string id;
    public string title;
    public string description;
    public string coverColor; // cover-1, cover-2, cover-3
    public List<string> genres;
    public int season;
    public int currentEpisode;
    public int totalEpisodes;
    public bool isActive; // true — доступно, false — "Скоро"
}