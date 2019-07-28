import { Injectable, TemplateRef } from '@angular/core';

@Injectable()
export class ToasterService {

  constructor() { }

  toasts: any[] = [];

  show(textOrTpl: string | TemplateRef<any>, options: any = {}) {
    const toast = { textOrTpl, ...options };
    this.toasts.push(toast);
    setTimeout(() => this.remove(toast), 4000);
  }

  remove(toast) {
    this.toasts = this.toasts.filter(t => t !== toast);
  }

}
