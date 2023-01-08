using AutoMapper;
using CourseLibrary.API.Models;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CourseLibrary.API.Controllers
{
    [ApiController]
    [Route("api/authors")]
    public class AuthorsController: ControllerBase  // or controller
    {
        private readonly ICourseLibraryRepository _courseLibraryRepository;

        private readonly IMapper _mapper;

        public AuthorsController(ICourseLibraryRepository courseLibraryRepository, IMapper mapper)
        {
            _courseLibraryRepository = courseLibraryRepository ??
                throw new ArgumentNullException(nameof(courseLibraryRepository));

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        ////[HttpGet("api/authors")]
        //public IActionResult GetAuthors()
        //{
        //    var authorsFromRepo = _courseLibraryRepository.GetAuthors();
        //    var authors = new List<AuthorDto>();

        //    foreach (var author in authorsFromRepo)
        //    {
        //        authors.Add(new AuthorDto()
        //        {
        //            Id = author.Id,
        //            Name = $"{author.FirstName} {author.LastName}",
        //            MainCategory = author.MainCategory,
        //            Age = author.DateOfBirth.GetCurrentAge()
        //        }); ;
        //    }

        //    //return new JsonResult(authorsFromRepo);
        //    return Ok(authorsFromRepo);
        //}

        [HttpGet()]
        [HttpHead]
        public ActionResult<IEnumerable<AuthorDto>> GetAuthors()
        {
            var authorsFromRepo = _courseLibraryRepository.GetAuthors();

            // replaced with automapper
            //var authors = new List<AuthorDto>();

            //foreach (var author in authorsFromRepo)
            //{
            //    authors.Add(new AuthorDto()
            //    {
            //        Id = author.Id,
            //        Name = $"{author.FirstName} {author.LastName}",
            //        MainCategory = author.MainCategory,
            //        Age = author.DateOfBirth.GetCurrentAge()
            //    }); ;
            //}
            //return Ok(authorsFromRepo);

            return Ok(_mapper.Map<IEnumerable<AuthorDto>>(authorsFromRepo));
        }


        [HttpGet("{authorId}")]
        public IActionResult GetAuthor(Guid authorId)
        {
            //if (!_courseLibraryRepository.AuthorExists(authorId))
            //    return NotFound();
            //var authorFromRepo = _courseLibraryRepository.GetAuthors().Where(a=>a.Id == authorId);

            var authorFromRepo = _courseLibraryRepository.GetAuthor(authorId);

            if (authorFromRepo == null)
                return NotFound();

            //return Ok(authorFromRepo);
            return Ok(_mapper.Map<AuthorDto>(authorFromRepo));
        }

    }
}
