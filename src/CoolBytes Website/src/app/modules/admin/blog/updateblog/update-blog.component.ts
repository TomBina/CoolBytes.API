import { ExternalLink } from '../../../../services/blogpostservice/external-link';
import { BlogPostUpdate } from '../../../../services/blogpostservice/blog-post-update';
import { Image } from '../../../../services/imagesservice/image';
import { BlogPostSummary } from '../../../../services/blogpostservice/blog-post-summary';
import { ImagesService } from '../../../../services/imagesservice/images.service';
import { BlogPostsService } from '../../../../services/blogpostservice/blog-posts.service';
import { AuthorsService } from '../../../../services/authorsservice/authors.service';
import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormArray, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { BlogPostPreview } from '../previewblog/blog-post-preview';
import { PreviewBlogComponent } from '../previewblog/preview-blog.component';
import { UpdateBlogPostCommand } from '../../../../services/blogpostservice/update-blog-post-command';

@Component({
    templateUrl: "./update-blog.component.html",
    styleUrls: ["./update-blog.component.css"]
})
export class UpdateBlogComponent implements OnInit, OnDestroy {
    constructor(
        private _route: ActivatedRoute,
        private _authorsService: AuthorsService,
        private _blogPostsService: BlogPostsService,
        private _router: Router,
        private _imagesService: ImagesService,
        private _formBuilder: FormBuilder) { }

    _form: FormGroup;
    _id: number;
    _blogPost: BlogPostSummary;
    _image: Image;
    _files: FileList;

    @ViewChild(PreviewBlogComponent)
    _previewBlogComponent: PreviewBlogComponent;
    _previewObserver: Subscription

    ngOnInit(): void {
        this._form = this._formBuilder.group({
            subject: ["", [Validators.required, Validators.maxLength(100)]],
            contentIntro: ["", [Validators.required, Validators.maxLength(100)]],
            content: ["", [Validators.required, Validators.maxLength(8000)]],
            tags: ["", Validators.maxLength(500)],
            externalLinks: this._formBuilder.array([])
        });

        this._previewObserver = this._form.valueChanges.subscribe(v => {
            this._previewBlogComponent.blogPostPreview
                = new BlogPostPreview(this._form.get("subject").value, this._form.get("contentIntro").value, this._form.get("content").value);
        })

        let id: number = parseInt(this._route.snapshot.paramMap.get("id"));
        this._blogPostsService.getUpdate(id).subscribe(blogPostUpdate => this.updateForm(blogPostUpdate));
    }

    ngOnDestroy(): void {
        if (this._previewObserver)
            this._previewObserver.unsubscribe();
    }

    growTextarea(element: HTMLTextAreaElement) {
        element.style.height = `${element.scrollHeight+2}px`;
    }

    getExternalLinksControls(): FormArray {
        return this._form.get("externalLinks") as FormArray;
    }

    addExternalLinkControl(): FormGroup {
        let links = this.getExternalLinksControls();
        let link = this.createExternalLinkFormGroup();
        links.push(link);

        return link;
    }

    createExternalLinkFormGroup(): FormGroup {
        return new FormGroup({
            name: new FormControl("", Validators.maxLength(50)),
            url: new FormControl("", Validators.maxLength(255))
        })
    }

    onImageSelectedHandler(image: Image) {
        this._form.get("content").setValue(`${this._form.get("content").value}![](${this._imagesService.getUri(image.uriPath)})`);
    }

    updateForm(blogPost: BlogPostUpdate) {
        this._id = blogPost.id;
        this._form.get("subject").setValue(blogPost.subject);
        this._form.get("contentIntro").setValue(blogPost.contentIntro);
        this._form.get("content").setValue(blogPost.content);
        this._image = blogPost.image;

        if (this._image)
            this._image.uri = this._imagesService.getUri(this._image.uriPath);

        let blogPostTags: string[] = [];
        blogPost.tags.forEach(t => {
            blogPostTags.push(t.name);
        });

        this._form.get("tags").setValue(blogPostTags.join(","))

        if (blogPost.externalLinks && blogPost.externalLinks.length > 0) {
            let externalLinks = blogPost.externalLinks;
            externalLinks.forEach(e => {
                let control = this.addExternalLinkControl();
                control.get("name").setValue(e.name);
                control.get("url").setValue(e.url);
            });
        }
        else {
            this.addExternalLinkControl();
        }
    }

    onFileChanged(element: HTMLInputElement) {
        this._files = element.files;
    }

    onSubmit(): void {
        if (!this._form.valid) {
            for (let controlName in this._form.controls) {
                this._form.get(controlName).markAsTouched();
            }
            return;
        }

        let updateBlogPostCommand = new UpdateBlogPostCommand();

        updateBlogPostCommand.id = this._id;
        updateBlogPostCommand.subject = this._form.get("subject").value;
        updateBlogPostCommand.content = this._form.get("content").value;
        updateBlogPostCommand.contentIntro = this._form.get("contentIntro").value;

        let externalLinks: ExternalLink[] = [];

        let controls = this.getExternalLinksControls();
        for (let control of controls.controls) {
            let externalLink = new ExternalLink(control.get("name").value, control.get("url").value);
            if (externalLink.name.length > 0 && externalLink.url.length > 0)
                externalLinks.push(externalLink);
        }

        updateBlogPostCommand.externalLinks = externalLinks;

        let tags: string = this._form.get("tags").value;

        if (tags.indexOf(",") !== -1 || tags.length > 0) {
            updateBlogPostCommand.tags = tags.split(",");
        }

        this._blogPostsService.update(updateBlogPostCommand, this._files).subscribe(blogpost => {
            this._router.navigateByUrl("admin/blogs")
        });
    }
}