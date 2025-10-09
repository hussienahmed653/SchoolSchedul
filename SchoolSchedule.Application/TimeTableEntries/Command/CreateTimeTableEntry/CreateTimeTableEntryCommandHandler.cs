using ErrorOr;
using SchoolSchedule.Application.Common.Interfaces;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
using SchoolSchedule.Domain;

namespace SchoolSchedule.Application.TimeTableEntries.Command.CreateTimeTableEntry
{
    public class CreateTimeTableEntryCommandHandler : IRequestHandler<CreateTimeTableEntryCommand, ErrorOr<Created>>
    {
        private readonly IUniteOfWork _unitOfWork;
        private readonly ITeacherAssignmentRepository _teacherAssignmentRepository;
        private readonly ITimeTableEntryRepository _timeTableEntryRepository;


        public CreateTimeTableEntryCommandHandler(IUniteOfWork unitOfWork,
                                                  ITeacherAssignmentRepository teacherAssignmentRepository,
                                                  ITimeTableEntryRepository timeTableEntry)
        {
            _unitOfWork = unitOfWork;
            _teacherAssignmentRepository = teacherAssignmentRepository;
            _timeTableEntryRepository = timeTableEntry;
        }

        public async Task<ErrorOr<Created>> Handle(CreateTimeTableEntryCommand request)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                //var teacherAssignments = await _teacherAssignmentRepository.GetAllAsync();
                ////نصاب كل مدرس
                //var TeacherWorkLoads = new Dictionary<int, int?>();
                //var timetableentry = new List<TimeTableEntry>();
                //foreach (var ta in teacherAssignments)
                //{
                //    if (ta.Teacher is null || ta.ClassSection is null)
                //        continue;

                //    var teacher = ta.Teacher;
                //    var classSection = ta.ClassSection;

                //    if (!TeacherWorkLoads.ContainsKey(teacher.TeacherId))
                //    {
                //        TeacherWorkLoads[teacher.TeacherId] = teacher.Workload;
                //    }
                //}
                //var FixedAssignments = teacherAssignments
                //    .Where(ta => ta.SubjectAssignment.Subject.IsFixed)
                //    .ToList();

                //foreach (var fa in FixedAssignments)
                //{
                //    var teacher = fa.Teacher;
                //    var classSection = fa.ClassSection;
                //    var subject = fa.SubjectAssignment.Subject;

                //    if (teacher == null || classSection == null || subject == null)
                //        continue;

                //    var dayid = subject.FixedDayId ?? 0;
                //    var period = subject.FixedPeriod;

                //    timetableentry.Add(new TimeTableEntry
                //    {
                //        TimeTableEntryId = await _timeTableEntryRepository.GetNextIdAsync(),
                //        TeacherAssignmentId = fa.TeacherAssignmentId,
                //        SubjectAssignmentId = fa.SubjectAssignmentId,
                //        DayId = dayid,
                //        Period = period,
                //    });

                //    if (TeacherWorkLoads.ContainsKey(teacher.TeacherId) && TeacherWorkLoads[teacher.TeacherId] > 0)
                //        TeacherWorkLoads[teacher.TeacherId]--;
                //}


                //Console.WriteLine("========== Teacher Workloads ==========");
                //foreach (var t in TeacherWorkLoads)
                //{
                //    Console.WriteLine($"TeacherId: {t.Key}, Remaining Workload: {t.Value}");
                //}

                //Console.WriteLine("\n========== Fixed Subjects Added ==========");
                //Console.WriteLine($"Total Fixed Entries: {timetableentry.Count}");

                //foreach (var entry in timetableentry)
                //{
                //    var ta = teacherAssignments.FirstOrDefault(x => x.TeacherAssignmentId == entry.TeacherAssignmentId);
                //    Console.WriteLine($"Class: {ta?.ClassSection?.SectionName} | Teacher: {ta?.Teacher?.TeacherName} | Subject: {ta?.SubjectAssignment?.Subject?.SubjectName} | Day: {entry.DayId} | Period: {entry.Period}");
                //}


                // 🧩 1. نجيب كل التخصيصات (TeacherAssignments)
                var teacherAssignments = await _teacherAssignmentRepository.GetAllAsync();

                // 🧠 2. نحضر نصاب كل مدرس
                var TeacherWorkLoads = new Dictionary<int, int?>();
                foreach (var ta in teacherAssignments)
                {
                    if (ta.Teacher is null)
                        continue;

                    var teacher = ta.Teacher;
                    if (!TeacherWorkLoads.ContainsKey(teacher.TeacherId))
                        TeacherWorkLoads[teacher.TeacherId] = teacher.Workload;
                }

                // 🧱 3. نجهز القوائم المتاحة لكل مدرس ولكل فصل (slots available)
                var availableSlotsForClass = new Dictionary<int, List<(int DayId, int Period)>>();
                var availableSlotsForTeacher = new Dictionary<int, List<(int DayId, int Period)>>();

                int totalDays = 5;
                int totalPeriods = 8;

                foreach (var ta in teacherAssignments)
                {
                    if (ta.Teacher is null || ta.ClassSection is null)
                        continue;

                    var teacherId = ta.Teacher.TeacherId;
                    var classId = ta.ClassSection.ClassSectionId;

                    if (!availableSlotsForClass.ContainsKey(classId))
                        availableSlotsForClass[classId] = new List<(int, int)>();
                    if (!availableSlotsForTeacher.ContainsKey(teacherId))
                        availableSlotsForTeacher[teacherId] = new List<(int, int)>();

                    for (int day = 1; day <= totalDays; day++)
                    {
                        for (int period = 1; period <= totalPeriods; period++)
                        {
                            availableSlotsForClass[classId].Add((day, period));
                            availableSlotsForTeacher[teacherId].Add((day, period));
                        }
                    }
                }

                // 📘 4. نحضر قائمة الجدول النهائي
                var timeTableEntries = new List<TimeTableEntry>();

                // 📗 5. نبدأ بالمواد الثابتة IsFixed = true
                var fixedAssignments = teacherAssignments
                    .Where(ta => ta.SubjectAssignment?.Subject?.IsFixed == true)
                    .ToList();

                foreach (var ta in fixedAssignments)
                {
                    var teacher = ta.Teacher;
                    var classSection = ta.ClassSection;
                    var subject = ta.SubjectAssignment?.Subject;
                    if (teacher is null || classSection is null || subject is null)
                        continue;

                    var teacherId = teacher.TeacherId;
                    var classId = classSection.ClassSectionId;

                    var dayId = subject.FixedDayId ?? 0;
                    var period = subject.FixedPeriod;

                    // تحقق إن القيم صالحة
                    if (dayId <= 0 || period <= 0)
                    {
                        Console.WriteLine($"[Fixed][SKIP] Subject {subject.SubjectName} has invalid FixedDayId/Period ({dayId}/{period})");
                        continue;
                    }

                    var slot = (DayId: dayId, Period: period);

                    // تحقق إن الـ slot ضمن الـ domain (لو بنيت availableSlots باستخدام real dayIds)
                    if (!availableSlotsForClass.ContainsKey(classId) || !availableSlotsForClass[classId].Contains(slot))
                    {
                        Console.WriteLine($"[Fixed][Conflict] Class {classSection.SectionName} cannot accept slot Day {dayId} Period {period} (not available in class slots).");
                        // هنا ممكن تعمل fallback: حاول نفس اليوم فترات أخرى أو سجل كـ conflict
                        continue;
                    }

                    if (!availableSlotsForTeacher.ContainsKey(teacherId) || !availableSlotsForTeacher[teacherId].Contains(slot))
                    {
                        Console.WriteLine($"[Fixed][Conflict] Teacher {teacher.TeacherName} not available at Day {dayId} Period {period} for {subject.SubjectName}.");
                        // خيار: تستخدم الـ placeholder "س" أو تحاول إيجاد مدرس آخر أو تحجز في مكان آخر
                        continue;
                    }

                    // لو كل شيء متاح — نحجز بالضبط الخانة المحددة
                    timeTableEntries.Add(new TimeTableEntry
                    {
                        TimeTableEntryGuid = Guid.NewGuid(),
                        TeacherAssignmentId = ta.TeacherAssignmentId,
                        SubjectAssignmentId = ta.SubjectAssignmentId,
                        DayId = dayId,
                        Period = period
                    });

                    // نحذف الخانة من المتاح للمدرس والفصل
                    availableSlotsForClass[classId].Remove(slot);
                    availableSlotsForTeacher[teacherId].Remove(slot);

                    // نخصم من النصاب
                    if (TeacherWorkLoads.ContainsKey(teacherId) && TeacherWorkLoads[teacherId] > 0)
                        TeacherWorkLoads[teacherId]--;
                }

                // 📕 6. المواد الزوجية (Even Subjects)
                var evenAssignments = teacherAssignments
                    .Where(ta => ta.SubjectAssignment?.EvenOrOdd == Key.Even && ta.SubjectAssignment.Subject.IsFixed == false)
                    .ToList();

                foreach (var ta in evenAssignments)
                {
                    var teacher = ta.Teacher;
                    var classSection = ta.ClassSection;
                    if (teacher is null || classSection is null)
                        continue;

                    var teacherId = teacher.TeacherId;
                    var classId = classSection.ClassSectionId;

                    if (!TeacherWorkLoads.ContainsKey(teacherId) || TeacherWorkLoads[teacherId] <= 0)
                        continue;

                    var classSlots = availableSlotsForClass[classId];
                    var teacherSlots = availableSlotsForTeacher[teacherId];

                    // نلاقي حصتين متتاليتين فاضيين في نفس اليوم
                    var validSlot = classSlots
                        .FirstOrDefault(slot =>
                            teacherSlots.Contains(slot) &&
                            classSlots.Contains((slot.DayId, slot.Period + 1)) &&
                            teacherSlots.Contains((slot.DayId, slot.Period + 1)));

                    if (validSlot == default)
                        continue;

                    // نضيف الحصتين
                    for (int i = 0; i < 2; i++)
                    {
                        var currentPeriod = validSlot.Period + i;

                        timeTableEntries.Add(new TimeTableEntry
                        {
                            TimeTableEntryGuid = Guid.NewGuid(),
                            TeacherAssignmentId = ta.TeacherAssignmentId,
                            SubjectAssignmentId = ta.SubjectAssignmentId,
                            DayId = validSlot.DayId,
                            Period = currentPeriod
                        });

                        classSlots.Remove((validSlot.DayId, currentPeriod));
                        teacherSlots.Remove((validSlot.DayId, currentPeriod));
                    }

                    TeacherWorkLoads[teacherId] -= 2;
                }

                // ✅ طباعة مؤقتة للتأكد
                Console.WriteLine("\n========== FIXED SUBJECTS ==========");
                var countfixed = 0;
                foreach (var entry in timeTableEntries.Where(e =>
                    fixedAssignments.Any(a => a.TeacherAssignmentId == e.TeacherAssignmentId)))
                {
                    var ta = teacherAssignments.First(x => x.TeacherAssignmentId == entry.TeacherAssignmentId);
                    Console.WriteLine($"[Fixed] {ta.ClassSection.SectionName} | {ta.Teacher.TeacherName} | {ta.SubjectAssignment.Subject.SubjectName} | Day {entry.DayId} | Period {entry.Period}");
                    countfixed++;
                }
                Console.WriteLine(countfixed);
                Console.WriteLine("\n========== EVEN SUBJECTS ==========");
                foreach (var entry in timeTableEntries.Where(e =>
                    evenAssignments.Any(a => a.TeacherAssignmentId == e.TeacherAssignmentId)))
                {
                    var ta = teacherAssignments.First(x => x.TeacherAssignmentId == entry.TeacherAssignmentId);
                    Console.WriteLine($"[Even] {ta.ClassSection.SectionName} | {ta.Teacher.TeacherName} | {ta.SubjectAssignment.Subject.SubjectName} | Day {entry.DayId} | Period {entry.Period}");
                }

                // ========== NON-FIXED SUBJECTS ==========
                Console.WriteLine("\n========== NON-FIXED SUBJECTS ==========");

                var nonFixedAssignments = teacherAssignments
                                        .Where(ta => ta.SubjectAssignment?.Subject?.IsFixed == false)
                                        .ToList();
                foreach (var ta in nonFixedAssignments)
                {
                    var teacher = ta.Teacher;
                    var classSection = ta.ClassSection;
                    var subject = ta.SubjectAssignment?.Subject;
                    if (teacher is null || classSection is null || subject is null)
                        continue;

                    var teacherId = teacher.TeacherId;
                    var classId = classSection.ClassSectionId;

                    if (!availableSlotsForClass.ContainsKey(classId) || !availableSlotsForTeacher.ContainsKey(teacherId))
                        continue;

                    // نحاول نلاقي أول خانة فاضية مشغولة لا في الفصل ولا المدرس
                    var slot = availableSlotsForClass[classId]
                        .FirstOrDefault(s => availableSlotsForTeacher[teacherId].Contains(s));

                    if (slot == default)
                    {
                        Console.WriteLine($"[Skip] Can't place {subject.SubjectName} for {teacher.TeacherName} in class {classSection.SectionName}");
                        continue;
                    }

                    // نضيفها في الجدول
                    timeTableEntries.Add(new TimeTableEntry
                    {
                        TimeTableEntryGuid = Guid.NewGuid(),
                        TeacherAssignmentId = ta.TeacherAssignmentId,
                        SubjectAssignmentId = ta.SubjectAssignmentId,
                        DayId = slot.DayId,
                        Period = slot.Period
                    });

                    // نحذف الخانة من المتاح
                    availableSlotsForClass[classId].Remove(slot);
                    availableSlotsForTeacher[teacherId].Remove(slot);

                    // نقلل النصاب
                    if (TeacherWorkLoads.ContainsKey(teacherId) && TeacherWorkLoads[teacherId] > 0)
                        TeacherWorkLoads[teacherId]--;

                    Console.WriteLine($"[Added] {subject.SubjectName} | {teacher.TeacherName} | {classSection.SectionName} | Day {slot.DayId} | Period {slot.Period}");
                }
                await _unitOfWork.CommitAsync();
                return Result.Created;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                return Error.Failure(description: "حدث خطأ أثناء انشاء الجدول");
            }
        }
    }
}
