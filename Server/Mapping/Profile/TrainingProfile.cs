using Riok.Mapperly.Abstractions;
using Server.Data.Entities;
using Server.Models.DTOs.Qualification;
using Server.Models.DTOs.Training;

namespace Server.Mapping.Profile
{
    [Mapper]
    public partial class TrainingProfile
    {
        public partial Training TrainingDTO_To_Training(TrainingDTO TrainingDTO);

        [MapProperty(nameof(Training.Employee.Name), nameof(TrainingDTO.EmployeeName))]
        public partial TrainingDTO Training_To_TrainingDTO(Training training);

        public partial IEnumerable<TrainingDTO> TrainingList_To_TrainingDTOList(IEnumerable<Training> Trainings);

        public partial Training CreateTraining_To_Training(CreateTrainingDTO createTrainingDTO);
    }
}
