using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using C_Sharp_Belt_II.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace C_Sharp_Belt_II.Controllers
{
    public class HomeController : Controller
    {
        private BeltExamContext _context;
        private string _controller;
        private string _action;

        public HomeController(BeltExamContext context)
        {
            _context = context;
            _controller = "User";
            _action = "Login";
        }

        [HttpGet]
        [Route("bright_ideas")]
        public IActionResult bright_ideas()
        {
            if(isLoggedIn()){
                setSessionViewData();
                
                return View(new IdeaBundleModel(_context));
            }else{
                return RedirectToAction(_action, _controller);
            }
        }

        [HttpPost]
        [Route("CreateIdea")]
        public IActionResult CreateIdea(Ideas idea){
            if(isLoggedIn()){
                setSessionViewData();

                if(ModelState.IsValid){

                    User user = _context.users.Include( j => j.Likes ).ThenInclude( p => p.LikedIdea ).SingleOrDefault(u => u.UserId == (int)ViewData["UserId"]);
                    
                    idea.IdeaCreator = user;
                    _context.ideas.Add(idea);
                    _context.SaveChanges();
                }
            }else{
                return RedirectToAction(_action, _controller);
            }

            return View("bright_ideas", new IdeaBundleModel(_context));
        }

        [HttpGet]
        [Route("bright_ideas/{IdeaId}")]
        public IActionResult bright_ideas(int IdeaId){
            if(isLoggedIn()){
                setSessionViewData();

                Ideas idea = _context.ideas.Include( l => l.Likes ).ThenInclude( u => u.LikedUser ).Include( c => c.IdeaCreator ).SingleOrDefault(i => i.IdeaId == IdeaId);

                if(idea != null){
                    if(idea.Likes != null){
                        idea.Likes = idea.Likes.GroupBy(a => a.LikedUser.Alias).Select(g => g.First()).ToList();
                    }

                    return View("like_status", idea);
                }else{
                    return View("bright_ideas", new IdeaBundleModel(_context));
                }
            }else{
                return RedirectToAction(_action, _controller);
            }
        }

        [HttpGet]
        [Route("users/{UserId}")]
        public IActionResult users(int UserId){
            if(isLoggedIn()){
                setSessionViewData();

                User user = _context.users.Include( l => l.Likes ).Include( i => i.Ideas ).SingleOrDefault(u => u.UserId == UserId);
                
                if(user != null){
                    return View(user);
                }else{
                    return RedirectToAction("bright_ideas");
                }
            }else{
                return RedirectToAction(_action, _controller);
            }
        }


        [HttpGet]
        [Route("Delete/{IdeaId}")]
        public IActionResult Delete(int IdeaId){
            if(isLoggedIn()){
                setSessionViewData();

                Ideas idea = _context.ideas.SingleOrDefault(u => u.IdeaId == IdeaId);

                if(idea != null){
                    if(idea.IdeaCreatorId == (int)ViewData["UserId"]){
                        _context.ideas.Remove(idea);   
                        _context.SaveChanges();
                    }
                }

                return View("bright_ideas", new IdeaBundleModel(_context));
            }else{
                return RedirectToAction(_action, _controller);
            }
        }

        [HttpGet]
        [Route("Like/{IdeaId}")]
        public IActionResult Like(int IdeaId)
        {
            if(isLoggedIn()){
                setSessionViewData();

                _context.likes.Add(new Likes{ IdeaId = IdeaId, UserId = (int)ViewData["UserId"] });
                _context.SaveChanges();

                return View("bright_ideas", new IdeaBundleModel(_context));
            }else{
                return RedirectToAction(_action, _controller);
            }
        }

        private void setSessionViewData()
        {
            ViewData["Username"] = HttpContext.Session.GetString("UserName");
            ViewData["UserId"] = (int)HttpContext.Session.GetInt32("UserId");
        }

        public bool isLoggedIn(){
            int? UserId = HttpContext.Session.GetInt32("UserId");
            return (UserId != null);
        }

    }
}
