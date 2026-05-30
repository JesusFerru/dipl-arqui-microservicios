import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from '../auth/login/login.component';
import { RegisterComponent } from '../auth/register/register.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, LoginComponent, RegisterComponent],
  template: `
    <div class="min-h-screen flex flex-col items-center justify-center bg-gray-100">
      <div class="text-center">
        <h1 class="text-3xl text-gray-800 mb-4">Bienvenido a la Plataforma</h1>
        <p class="text-gray-600 mb-6">Por favor inicia sesión para continuar</p>
        <button 
          (click)="showLoginModal = true"
          class="bg-blue-600 text-white py-2 px-6 rounded">
          Ingresar
        </button>
      </div>

      <app-login 
        *ngIf="showLoginModal" 
        (close)="showLoginModal = false"
        (goToRegister)="showLoginModal = false; showRegisterModal = true">
      </app-login>
      
      <app-register
        *ngIf="showRegisterModal"
        (close)="showRegisterModal = false"
        (goToLogin)="showRegisterModal = false; showLoginModal = true">
      </app-register>
    </div>
  `
})
export class HomeComponent {
  showLoginModal = false;
  showRegisterModal = false;
}
