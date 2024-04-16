using Microsoft.AspNetCore.SignalR.Protocol;
using StudentApplication.Models;
using StudentApplication.Repositories;

namespace StudentApplication.Services
{

    public interface ISubjectService
    {
        Task<List<Subject>> GetSubjects();
        Task<Subject> GetSubject(int SubjectId);
        Task<Subject> CreateSubject(int ClassId, string SubjectName, string Subjectcode, Teacher teacher);
        Task<Subject> UpdateSubject(int SubjectId, int ClassId, string? SubjectName = null, string? Subjectcode = null, Teacher teacher = null);
        Task<Subject> DeleteSubject(int SubjectId, bool ActiveSubject = false);
    }

    public class SubjectService : ISubjectService
    {

        private readonly ISubjectRepository _subjectRepository;

        public SubjectService(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        public async Task<Subject> CreateSubject(int ClassId, string SubjectName, string Subjectcode, Teacher teacher)
        {
            return await _subjectRepository.CreateSubject(ClassId, SubjectName, Subjectcode, teacher);
        }

        public async Task<Subject> GetSubject(int ClassId)
        {
            return await _subjectRepository.GetSubject(ClassId);
        }

        public async Task<List<Subject>> GetSubjects()
        {
            return await _subjectRepository.GetSubjects();
        }

        public async Task<Subject> UpdateSubject(int subjectId, int classId, string? subjectName = null, string? subjectCode = null, Teacher teacher = null)
        {
            Subject subject = await _subjectRepository.GetSubject(subjectId);
            
            if (subjectId <= 0)
            {
                throw new ArgumentException("Subject ID debe ser numero positivo.");
            }

            if (subject == null)
            {
                return null;
            }
            if (subjectName != null)
            {
                subject.SubjectName = subjectName;
            }
            if (subjectCode != null)
            {
                subject.Subjectcode = subjectCode;
            }
            if (teacher != null)
            {
                subject.Teacher = teacher;
            }
            subject.ClassId = classId;

            return await _subjectRepository.UpdateSubject(subject);
        }
        public async Task<Subject> DeleteSubject(int SubjectId, bool ActiveSubject)
        {
            Subject subject = await _subjectRepository.GetSubject(SubjectId);
            if (subject == null)
            {
                return null;
            }
            if (ActiveSubject)
            {
                subject.ActiveSubject = ActiveSubject;
            }
            await _subjectRepository.DeleteSubject(SubjectId,ActiveSubject);
            return subject;
        }

    }
}