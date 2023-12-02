using DesignPlatform.ViewModels.CustomerViewModels;
using Microsoft.AspNetCore.Http;

namespace DesignPlatform.Areas.Employee.ViewModels.EmployeeProjectsViewModels
{
    public class EmployeeProjectsDetailsViewModel
    {
        public int Id { get; set; }
        public string Phone { get; set; }
        public string AppointmentPhone { get; set; }
        public DateTime ScheduleDate { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; }
        public string DesignImagePath { get; set; }
        public int Status { get; set; }
        public string StatusText { get; set; }
        public string ClientId { get; set; }
        public string ClientName { get; set; }
        public string ProjectManagerId { get; set; }
        public string DesignerId { get; set; }
        public string DesignerName { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public int? CountryId { get; set; }
        public string CountryName { get; set; }
        public int? StateId { get; set; }
        public string StateName { get; set; }

        public string YouWantADesignForWahtAreaAnswer { get; set; }
        public string YouWantADesignForWahtAreaNotes { get; set; }

        public string WhatSortOfStyleAnswer { get; set; }
        public string WhatSortOfStyleNotes { get; set; }

        public string DoYouHaveKidsAnswer { get; set; }
        public string DoYouHaveKidsNotes { get; set; }

        public string DoyouEntertainALotAnswer { get; set; }
        public string DoyouEntertainALotNotes { get; set; }

        public string IsThePropertyVisibleOnGoogleMapsAnswer { get; set; }
        public string IsThePropertyVisibleOnGoogleMapsNotes { get; set; }

        public string WhatYourAreLookingToKeepAnswer { get; set; }
        public string WhatYourAreLookingToKeepNotes { get; set; }

        public string WouldYouLikeToAddHardscapeAnswer { get; set; }
        public string WouldYouLikeToAddHardscapeNotes { get; set; }

        public string RegardingTheAmountOfPlantsAnswer { get; set; }
        public string RegardingTheAmountOfPlantsNotes { get; set; }

        public string DoYouWantAWaterFeatureAnswer { get; set; }
        public string DoYouWantAWaterFeatureNotes { get; set; }

        public string DoYouWantABBQAreaAnswer { get; set; }
        public string DoYouWantABBQAreaNotes { get; set; }

        public string DoYoWantToHaveAFirePitAreaAnswer { get; set; }
        public string DoYoWantToHaveAFirePitAreaNotes { get; set; }

        public string DoYouWantAGrassAreaAnswer { get; set; }
        public string DoYouWantAGrassAreaNotes { get; set; }

        public string DoYouLikeColorfulOrSimpleGreenAndWhitePlantsAnswer { get; set; }
        public string DoYouLikeColorfulOrSimpleGreenAndWhitePlantsNotes { get; set; }

        public string DoYouWantAnyPrivacyAnswer { get; set; }
        public string DoYouWantAnyPrivacyNotes1 { get; set; }
        public string DoYouWantAnyPrivacyNotes2 { get; set; }

        public string DoYouHaveOrWantAPergolaOrCoveredPatioAreaAnswer { get; set; }
        public string DoYouHaveOrWantAPergolaOrCoveredPatioAreaNotes { get; set; }

        public string FYWhatAreYouLookingToKeepOrRemoveInYourYardAnswer { get; set; }
        public string FYWhatAreYouLookingToKeepOrRemoveInYourYardNotes { get; set; }

        public string FYWouldYouLikeToAddAnyTypeOfHardscapeAnswer { get; set; }
        public string FYWouldYouLikeToAddAnyTypeOfHardscapeNotes { get; set; }

        public string FYRegardingTheAmountOfPlantsAreYouLookingForSomethingMoreAnswer { get; set; }
        public string FYRegardingTheAmountOfPlantsAreYouLookingForSomethingMoreNotes { get; set; }

        public string FYDoYouWantToDoAnythingDifferentWithYourEntranceAnswer { get; set; }
        public string FYDoYouWantToDoAnythingDifferentWithYourEntranceNotes { get; set; }

        public string FYDoYouWantAWaterFeatureAnswer { get; set; }
        public string FYDoYouWantAWaterFeatureNotes { get; set; }

        public string FYDoYouWantAGrassAreaAnswer { get; set; }
        public string FYDoYouWantAGrassAreaNotes { get; set; }

        public string FYDoYouWantAnyTallTreesInYourFrontYardAnswer { get; set; }
        public string FYDoYouWantAnyTallTreesInYourFrontYardNotes { get; set; }

        public string FYDoYouLikeColorfulOrSimpleGreenAndWhitePlantsAnswer { get; set; }
        public string FYDoYouLikeColorfulOrSimpleGreenAndWhitePlantsNotes { get; set; }

        public string FYDoYouWantAnyPrivacyAnswer { get; set; }
        public string FYDoYouWantAnyPrivacyNotes1 { get; set; }
        public string FYDoYouWantAnyPrivacyNotes2 { get; set; }

        public string FYDoYouHaveASideYardAnswer { get; set; }
        public string FYDoYouHaveASideYardNotes { get; set; }

        public string FYWhatIsYourBudgetForYourYardRenovationAnswer { get; set; }
        public string FYWhatIsYourBudgetForYourYardRenovationNotes { get; set; }

        public string FYWhenDoYouPlanOnStartingYourYardRenovationAnswer { get; set; }
        public string FYWhenDoYouPlanOnStartingYourYardRenovationNotes { get; set; }

        public string SummaryNotes { get; set; }
        public string DesignerSkillsSet { get; set; }
        public string ScopeAndSizeOfProject { get; set; }
        public string AppointmentStatus { get; set; }

        public string DesignStatus { get; set; }
        public string DesignerNotes { get; set; }

        public decimal Price { get; set; }
        public string Area { get; set; }
        public bool IsClientUploadResources { get; set; }

        public string PackageName { get; set; }

        public List<string> Images { get; set; }
        public List<string> Inspirations { get; set; }
        public IFormFile[] DocumentFiles { get; set; }
        public IFormFile[] DesignFiles { get; set; }
        public List<ImageViewModel> DocumentLinks { get; set; }
        public List<ImageViewModel> DesignLinks { get; set; }

    }
}
