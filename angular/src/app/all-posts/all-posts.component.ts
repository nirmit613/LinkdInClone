import { Component, OnInit } from '@angular/core';
import { IPost } from '../interfaces/post';
import { PostService } from '../services/post.service';
import { LikecommentService } from '../services/likecomment.service';

@Component({
  selector: 'app-all-posts',
  templateUrl: './all-posts.component.html',
  styleUrl: './all-posts.component.scss',
})
export class AllPostsComponent implements OnInit {
  public PostsList!: IPost[];

  constructor(private postService: PostService, private likecommentService: LikecommentService) {}

  ngOnInit(): void {
    this.getPostsOfConnectedUsers();
  }

  public getPostsOfConnectedUsers() {
    this.postService.getPostOfConnectedUsers().subscribe({
      next: res => {
        console.log(res);
        this.PostsList = res.data;
        this.updateTotalLikes();
      },
      error: error => {
        console.log('Some Error occurs: ', error);
      },
    });
  }

  public updateTotalLikes() {
    this.PostsList.forEach(post => {
      this.likecommentService.getTotalLikesByPostId(post.id).subscribe({
        next: res => {
          post.totalLikes = res.data;
        },
        error: error => {
          console.log('Error fetching total likes: ', error);
        },
      });
      this.likecommentService.isPostLikeByUser(post.id).subscribe({
        next: res => {
          post.liked = res;
        },
        error: error => {
          console.log('Error checking if post is liked by user: ', error);
        },
      });
    });
  }
  public toggleLike(post: IPost) {
    if (post.liked) {
      this.likecommentService.unLikePost(post.id).subscribe({
        next: () => {
          post.liked = false;
          this.updateTotalLikes();
        },
        error: error => {
          console.log('Error unliking post: ', error);
        },
      });
    } else {
      this.likecommentService.addLikePost(post.id).subscribe({
        next: () => {
          post.liked = true;
          this.updateTotalLikes();
        },
        error: error => {
          console.log('Error liking post: ', error);
        },
      });
    }
  }
  public toggleCommentSection(post: IPost) {
    post.showComments = !post.showComments;
    if (post.showComments && !post.comments) {
      this.likecommentService.getCommentsOfPost(post.id).subscribe({
        next: res => {
          post.comments = res.data;
        },
        error: error => {
          console.log('Error fetching comments: ', error);
        },
      });
    }
  }

  public addComment(post: IPost, content: string) {
    this.likecommentService.addComment({ postId: post.id, content }).subscribe({
      next: () => {
        post.newComment = '';
        post.showComments = false;
        this.likecommentService.getCommentsOfPost(post.id).subscribe({
          next: res => {
            post.comments = res.data;
            console.log(post.comments);
          },
          error: error => {
            console.log('Error fetching comments: ', error);
          },
        });
      },
      error: error => {
        console.log('Error adding comment: ', error);
      },
    });
  }
}
