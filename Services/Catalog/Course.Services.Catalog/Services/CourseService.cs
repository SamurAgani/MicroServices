using AutoMapper;
using Course.Services.Catalog.Dtos;
using Course.Services.Catalog.Models;
using Course.Services.Catalog.Settings;
using FreeCource.Shared.Dtos;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Services.Catalog.Services
{
    public class CourseService : ICourseService
    {
        private readonly IMongoCollection<Models.Course> _courseCollection;
        private readonly IMongoCollection<Models.Category> _categoryCollection;
        private readonly IMapper _mapper;
        public CourseService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var dataBase = client.GetDatabase(databaseSettings.DataBaseName);

            _courseCollection = dataBase.GetCollection<Models.Course>(databaseSettings.CourseCollectionName);
            _categoryCollection = dataBase.GetCollection<Models.Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }
        public async Task<Response<List<CourseDto>>> GetAllAsync()
        {
            var courses = await _courseCollection.Find(courses => true).ToListAsync();
            if (courses.Any()) 
            {
                foreach (var course in courses)
                {
                    course.Category = await _categoryCollection.Find<Category>(x => x.Id == course.CategoryId).FirstOrDefaultAsync();
                }
            }
            else
            {
                courses = new List<Models.Course>();
            }
            return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);
        }

        public async Task<Response<CourseDto>> GetById(string Id)
        {
            Models.Course course = await _courseCollection.Find<Models.Course>(x => x.Id == Id).FirstOrDefaultAsync(); ;
            if(course is null)
            {
                return Response<CourseDto>.Fail("Not found",404);
            }
            course.Category = await _categoryCollection.Find<Category>(x => x.Id == course.CategoryId).FirstOrDefaultAsync();
            return Response<CourseDto>.Success(_mapper.Map<CourseDto>(course),200);
        }

        public async Task<Response<List<CourseDto>>> GetByUserId(string userId)
        {
            var courses = await _courseCollection.Find(courses => courses.UserId == userId).ToListAsync();
            if (courses.Any())
            {
                foreach (var course in courses)
                {
                    course.Category = await _categoryCollection.Find<Category>(x => x.Id == course.CategoryId).FirstOrDefaultAsync();
                }
            }
            else
            {
                courses = new List<Models.Course>();
            }
            return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);
        }
        public async Task<Response<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto)
        {
            var newCourse = _mapper.Map<Models.Course>(courseCreateDto);
            newCourse.CreatedDate = DateTime.Now;
            await _courseCollection.InsertOneAsync(newCourse);
            return Response<CourseDto>.Success(_mapper.Map<CourseDto>(newCourse), 200);
        }


        public async Task<Response<NoContent>> UpdateAsync(CourseUpdateDto courseUpdateDto)
        {
            var updateCourse = _mapper.Map<Models.Course>(courseUpdateDto);
            var result = await _courseCollection.FindOneAndReplaceAsync(x => x.Id == courseUpdateDto.Id, updateCourse);

            if(result is null)
            {
                return Response<NoContent>.Fail("Course not found", 404);
            }
            return Response<NoContent>.Success(204);
        }

        public async Task<Response<NoContent>> DeleteAsync(string Id)
        {
            var result = await _courseCollection.DeleteOneAsync(x => x.Id == Id);
            if(result.DeletedCount > 0)
            {
                return Response<NoContent>.Fail("Not Found", 404);
            }
            return Response<NoContent>.Success(204);

        }
    }
}
