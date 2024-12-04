

namespace API.DTO
{
    public class TeacherScheduleDTO
    {
        public int? Id { get; set; }

        public TimeSpan StudyTime { get; set; }

        public TimeSpan StudyTimeEnd { get; set; }

        public string ClassRoom { get; set; } = null!;

        public DayOfWeek StudyDay { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string UserId { get; set; } = null!;

        public string ClassId { get; set; } = null!;

        public string SubjectId { get; set; } = null!;

    }
}
