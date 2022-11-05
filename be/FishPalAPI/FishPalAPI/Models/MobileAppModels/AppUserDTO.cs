namespace FishPalAPI.Models.MobileAppModels
{
    public class AppUserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Role { get; set; }
        public string Facet { get; set; }

        public string Province { get; set; }

        public string Club { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string ValidUntil { get; set; }

        public FederationDTO Federations { get; set; }

        public ClubDTO Clubs { get; set; }
        public FederationDTO Federation { get; set; }

    }
}
