import { Component, OnInit } from '@angular/core';
import { IPost } from '../interfaces/post';
import { PostService } from '../services/post.service';
import { AddPostFormComponent } from '../add-post-form/add-post-form.component';
import { MatDialog } from '@angular/material/dialog';
import { LikecommentService } from '../services/likecomment.service';

@Component({
  selector: 'app-my-posts',
  templateUrl: './my-posts.component.html',
  styleUrl: './my-posts.component.scss',
})
export class MyPostsComponent implements OnInit {
  public PostsList!: IPost[];
  constructor(
    private postService: PostService,
    public dialog: MatDialog,
    private likecommentService: LikecommentService
  ) {}

  ngOnInit(): void {
    this.getPostByUserId();
  }

  public getAllPosts() {
    this.postService.getAllPosts().subscribe({
      next: res => {
        console.log(res);
        this.PostsList = res.data;
      },
      error: error => {
        console.log('Some Error occurs: ', error);
      },
    });
  }
  public getPostByUserId() {
    this.postService.getPostsByUserId().subscribe({
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
  public addPost(): void {
    let post: IPost = {
      id: '',
      content: '',
      imageUrl: null,
      userId: '',
      creationTime: '',
      user: {
        userName: '',
      },
    };
    const dialogRef = this.dialog.open(AddPostFormComponent, {
      data: post,
      width: '50%',
    });
    dialogRef.afterClosed().subscribe({
      next: res => {
        console.log('Add Post result:', res);
        this.getPostByUserId();
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
