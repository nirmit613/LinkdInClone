import { Component, Inject } from '@angular/core';
import { FormArray, FormControl, FormGroup, Validators } from '@angular/forms';
import { IPost } from '../interfaces/post';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material/dialog';
import { PostService } from '../services/post.service';

@Component({
  selector: 'app-add-post-form',
  templateUrl: './add-post-form.component.html',
  styleUrl: './add-post-form.component.scss',
})
export class AddPostFormComponent {
  imageUrl: any;
  file: any;
  public addPostForm!: FormGroup;

  constructor(
    public dialogRef: MatDialogRef<AddPostFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: IPost,
    public dialog: MatDialog,
    private postService: PostService
  ) {}
  ngOnInit(): void {
    this.initializeForm();
  }
  initializeForm(): void {
    this.addPostForm = new FormGroup({
      id: new FormControl(this.data.id ?? null),
      content: new FormControl(this.data.content, Validators.required),
      imageUrl: new FormControl(this.data.imageUrl, Validators.required),
    });
  }

  onFileSelected(event: any): void {
    const fileInput = event.target as HTMLInputElement;
    const file = fileInput.files?.[0];
    if (file) {
      this.file = file;
      this.addPostForm.patchValue({ imageUrl: file });
    }
  }

  public addPost() {
    const contentValue = this.addPostForm.get('content').value;
    const imageUrlValue = this.addPostForm.get('imageUrl').value;

    if (!contentValue && !imageUrlValue) {
      alert('Please enter post content or select a post image.');
      return;
    }
    const postData = this.addPostForm.value;
    console.log('postData : ', postData);
    const formData = new FormData();
    formData.append('content', postData.content);
    formData.append('imageUrl', this.file);
    this.postService.addPost(formData).subscribe({
      next: (res: any) => {
        console.log('Add Post Response:', res);
        this.dialogRef.close(this.addPostForm.value);
        this.file = null;
      },
      error: (err: any) => {
        console.error('Error:', err);
      },
    });
  }
  public close() {
    this.dialogRef.close();
  }
}
