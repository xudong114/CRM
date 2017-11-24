using AutoMapper;
using Ingenious.Application.Interface;
using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using Ingenious.DTO;
using Ingenious.Infrastructure;
using Ingenious.Infrastructure.Enum;
using Ingenious.Infrastructure.Helper;
using Ingenious.Repositories;
using Ingenious.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace Ingenious.Application.Implement
{
    public class G_FileService : ApplicationService, IG_FileService
    {
        private readonly IG_FileRepository _IG_FileRepository;
        public G_FileService(IRepositoryContext context,
            IG_FileRepository iG_FileRepository)
            : base(context)
        {
            this._IG_FileRepository = iG_FileRepository;
        }

        public G_FileDTOList GetFilesByReferenceId(Guid referenceId)
        {
            var list = new G_FileDTOList();
            this._IG_FileRepository.Data.Where(item => item.IsActive && item.ReferenceId.Equals(referenceId)).ToList().ForEach(item =>
            {
                list.Add(Mapper.Map<G_File, G_FileDTO>(item));
            });

            return list;
        }

        public G_FileDTOList GetFilesByReferenceId(Guid referenceId, string code = "")
        {
            var list = new G_FileDTOList();
            this._IG_FileRepository.Data.Where(item => item.IsActive && item.ReferenceId.Equals(referenceId) && (code == "" || item.Code.Equals(code))).ToList().ForEach(item =>
            {
                list.Add(Mapper.Map<G_File, G_FileDTO>(item));
            });

            return list;
        }

        public G_FileDTO GetByKey(System.Guid id)
        {
            var file = this._IG_FileRepository.GetByKey(id);

            var fileDTO = Mapper.Map<G_File, G_FileDTO>(file);

            return fileDTO;
        }

        public G_FileDTO Create(G_FileDTO dto)
        {
            var url = System.Web.HttpContext.Current.Request.Url;
            var fullPath = string.Format("{0}://{1}", url.Scheme, url.Authority);

            var storePath = Path.Combine("/uploads", string.Format("{0}", DateTime.Now.ToString("yyyy-MM-dd")));
            storePath = fullPath + storePath;
            var path = Path.Combine(dto.Path, string.Format("{0}", DateTime.Now.ToString("yyyy-MM-dd")));
            this.CreateFile(dto.Data, dto.Name, path);
            dto.Path = Path.Combine(storePath, dto.Name);
            return base.F_Create<G_FileDTO, G_File>(dto
                , _IG_FileRepository
                , dtoAction => { });
        }

        private void CreateFile(Stream stream, string fileName, string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            fileName = Path.Combine(path, fileName);
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            stream.Seek(0, SeekOrigin.Begin);

            FileStream fs = new FileStream(fileName, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(bytes);
            bw.Close();
            fs.Close();
        }

        public List<G_FileDTO> Update(System.Collections.Generic.List<G_FileDTO> dtoList)
        {
            return base.F_Update<G_FileDTO, List<G_FileDTO>, G_File>(dtoList
                , _IG_FileRepository
                , dto => dto.Id
                , (dto, entity) =>
                {
                    entity.ReferenceId = dto.ReferenceId;
                    entity.ModifiedDate = dto.ModifiedDate;
                    entity.ModifiedBy = dto.ModifiedBy;
                });
        }


        public void Delete(System.Collections.Generic.List<G_FileDTO> dtoList)
        {
            base.F_Update<G_FileDTO, List<G_FileDTO>, G_File>(dtoList
                , _IG_FileRepository
                , dto => dto.Id
                , (dto, entity) =>
                {
                    entity.IsActive = dto.IsActive;
                    entity.ModifiedDate = dto.ModifiedDate;
                    entity.ModifiedBy = dto.ModifiedBy;
                });
        }

    }
}
