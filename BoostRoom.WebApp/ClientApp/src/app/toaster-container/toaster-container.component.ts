import { Component, OnInit } from '@angular/core';
import { ToasterService } from '../services/toaster.service';

@Component({
  selector: 'app-toaster-container',
  templateUrl: './toaster-container.component.html',
  styleUrls: ['./toaster-container.component.css']
})
export class ToasterContainerComponent implements OnInit {

  constructor(public toastService: ToasterService) { }

  ngOnInit() {
  }

}
