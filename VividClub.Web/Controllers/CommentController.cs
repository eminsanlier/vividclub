using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VividClub.Services;
using VividClub.Web.Extensions;
using VividClub.Web.Infrastructure;
using VividClub.Web.Infrastructure.Filters;
using VividClub.Web.Models.Comment;

namespace VividClub.Web.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        private readonly IPostService postService;
        private readonly ICommentService commentService;

        public CommentController(IPostService postService, ICommentService commentService)
        {
            this.postService = postService;
            this.commentService = commentService;
        }

        public IActionResult Create(int postId)
        {
            var postCommentViewModel = this.postService.PostById(postId);

            PostCommentCreateModel postCommentCreateModel = Mapper.Map<PostCommentCreateModel>(postCommentViewModel);

            return this.ViewOrNotFound(postCommentCreateModel);
        }

        [HttpPost]
        [ValidateModelState]
        public IActionResult Create(PostCommentCreateModel model, string returnUrl = null)
        {
            if (CoreValidator.CheckIfStringIsNullOrEmpty(model.CommentText))
            {
                ModelState.AddModelError(string.Empty, "You cannot submit an empty comment!");
                return this.ViewOrNotFound(model);
            }

            this.commentService.Create(model.CommentText, User.GetUserId(), model.Id);
            return RedirectToAction("Index", "Users");
        }
    }
}