import { Component, Input, Output, EventEmitter } from "@angular/core";
import { BlogPostSummary } from "../../services/blog-post-summary";

@Component({
    selector: "home-blog-post-intro",
    templateUrl: "./blog-post-intro.component.html",
    styleUrls: ["blog-post-intro.component.css"]
})
export class BlogPostIntroComponent { 
    @Input()
    blogPost: BlogPostSummary;

    @Input()
    cssClass: string;

    @Output()
    onBlogPostMouseEnter = new EventEmitter<BlogPostSummary>();
    
    @Output()
    onBlogPostMouseLeave = new EventEmitter<BlogPostSummary>();

    onBlogPostMouseEnterHandler() {
        this.onBlogPostMouseEnter.emit(this.blogPost);
    }

    onBlogPostMouseLeaveHandler() {
        this.onBlogPostMouseLeave.emit(this.blogPost);
    }
}