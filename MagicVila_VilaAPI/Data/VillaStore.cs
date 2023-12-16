using MagicVila_VilaAPI.Models.Dto;

namespace MagicVila_VilaAPI.Data
{
    public static class VillaStore
    {
        public static List<VillaDTO> villaList =
        new List<VillaDTO> {
                new VillaDTO{Id=1, Name="Pool view", Sqft=100, Occupancy=2},
                new VillaDTO{Id=2, Name="Beach view", Sqft=200, Occupancy=4},
                new VillaDTO{Id=3, Name="Point view", Sqft=300, Occupancy=5},
                new VillaDTO{Id=4, Name="Up view", Sqft=150, Occupancy=3}
            };

    };
}