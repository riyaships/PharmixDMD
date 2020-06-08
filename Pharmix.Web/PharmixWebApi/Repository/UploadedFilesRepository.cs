using PharmixWebApi.Context;
using PharmixWebApi.Model;
using PharmixWebApi.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmixWebApi.Repository
{
    public class UploadedFilesRepository: IUploadedFilesRepository<UploadedFiles, int>
    {
        ApplicationContext _context;
        public UploadedFilesRepository(ApplicationContext Context)
        {
            _context = Context;
        }

        public UploadedFiles Get(int id)
        {
            var uploadedFiles = _context.UploadedFiles.FirstOrDefault();
            return uploadedFiles;
        }

        public IEnumerable<UploadedFiles> GetAll()
        {
            var uploadedFiles = _context.UploadedFiles.ToList();
            return uploadedFiles;
        }

        public int Add(UploadedFiles stundent)
        {
            _context.UploadedFiles.Add(stundent);
            int studentID = _context.SaveChanges();
            return studentID;
        }
    }
}
