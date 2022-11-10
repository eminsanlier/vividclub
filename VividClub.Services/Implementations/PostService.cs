using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using VividClub.Data;
using VividClub.Data.Entities;
using VividClub.Services.Infrastructure.CustomDataStructures;
using VividClub.Services.Models;

namespace VividClub.Services
{
    public class PostService : IPostService
    {
        private readonly VividClubDbContext db;
        private readonly IPhotoService photoService;
        //private readonly ICommentService commentService;

        public PostService(VividClubDbContext db, IPhotoService photoService)
        {
            this.db = db;
            this.photoService = photoService;
            //this.commentService = commentService;
        }

        public void Create(string userId, string text, IFormFile photo)
        {
            var post = new Post
            {
                UserId = userId,
                Text = text,
                Likes = 0,
                Date = DateTime.UtcNow,
                Photo = photo != null ? this.photoService.PhotoAsBytes(photo) : null
            };

            db.Posts.Add(post);
            db.SaveChanges();
        }

        public void Delete(int postId)
        {
            var post = this.db.Posts.Find(postId);
            //this.commentService.DeleteCommentsByPostId(postId);
            this.db.Remove(post);
            this.db.SaveChanges();
        }

        public void Edit(int postId, string text, IFormFile photo)
        {
            var post = this.db.Posts.Find(postId);
            post.Text = text;
            post.Photo = photo != null ? this.photoService.PhotoAsBytes(photo) : null;
            this.db.SaveChanges();
        }

        public bool Exists(int id) => this.db.Posts.Any(p => p.Id == id);

        public PaginatedList<PostModel> ClubPostsByUserId(string userId, int pageIndex, int pageSize)
        {
            var posts = this.db
                .Posts
                .Where(p => p.UserId == userId)
                //.Include(p => p.Comments)
                //.ThenInclude(p => p.User)
                .ProjectTo<PostModel>()
                .OrderByDescending(p => p.Date);

            return posts != null ? PaginatedList<PostModel>.Create(posts, pageIndex, pageSize) : null;
        }

        public void Like(int postId)
        {
            if (this.Exists(postId))
            {
                var post = this.db.Posts.Find(postId);
                post.Likes++;
                this.db.SaveChanges();
            }
        }

        public PostModel PostById(int postId)
        {
            return this.db.Posts.Where(p => p.Id == postId).ProjectTo<PostModel>().FirstOrDefault();
        }

        public PaginatedList<PostModel> PostsByUserId(string userId, int pageIndex, int pageSize)
        {
            var posts = this.db
                .Posts
                .Where(p => p.UserId == userId)
                .ProjectTo<PostModel>()
                .OrderByDescending(p => p.Date);

            return posts != null ? PaginatedList<PostModel>.Create(posts.AsNoTracking(), pageIndex, pageSize) : null;
        }

        public bool UserIsAuthorizedToEdit(int postId, string userId) => this.db.Posts.Any(p => p.Id == postId && p.UserId == userId);

    }
}