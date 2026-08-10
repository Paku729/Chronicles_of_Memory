using System.Collections.Generic;

public static class StoryRepository
{
    public static List<StoryData> GetStories()
    {
        return new List<StoryData>
        {
            new StoryData
            {
                id = "1",
                title = "Полынная невеста",
                description = "Свадьба по расчёту связывает её с чужаком из северных земель...",
                coverColor = "cover-1",
                genres = new List<string> { "Романтика", "Драма" },
                season = 1,
                currentEpisode = 4,
                totalEpisodes = 10,
                isActive = true
            },
            new StoryData
            {
                id = "2",
                title = "Дом на грани тумана",
                description = "Старый особняк хранит память о тех, кто не смог его покинуть...",
                coverColor = "cover-2",
                genres = new List<string> { "Мистика", "Романтика" },
                season = 1,
                currentEpisode = 1,
                totalEpisodes = 8,
                isActive = false
            },
            new StoryData
            {
                id = "3",
                title = "Соль и сталь",
                description = "На границе двух королевств встречаются капитан наёмников и...",
                coverColor = "cover-3",
                genres = new List<string> { "Экшн", "Романтика" },
                season = 2,
                currentEpisode = 2,
                totalEpisodes = 12,
                isActive = true
            }
        };
    }
}