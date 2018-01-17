using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using TaskManager.Models;
using TaskManager.Dtos;

namespace TaskManager.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Members, MembersDto>();
            Mapper.CreateMap<MemberPositions, MemberPositionDto>();
            Mapper.CreateMap<MemberGroups, MemberGroupDto>();
            Mapper.CreateMap<Tasks, TaskDto>();
            Mapper.CreateMap<TaskCategories, TaskCategoryDto>();
            Mapper.CreateMap<TaskTypes, TaskTypeDto>();
            Mapper.CreateMap<TaskStatuses, TaskStatusDto>();
            Mapper.CreateMap<Companies, CompanyDto>();
            Mapper.CreateMap<Prices, PriceDto>();
            Mapper.CreateMap<TaskProcedures, TaskProcedureDto>();
            Mapper.CreateMap<SubTasksLevel1, SubtaskLevel1Dto>();
            Mapper.CreateMap<Notes, NoteDto>();
        }
    }
}