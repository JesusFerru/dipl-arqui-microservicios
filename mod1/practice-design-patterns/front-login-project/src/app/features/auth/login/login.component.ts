import { Component, EventEmitter, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../../core/auth/auth.service';
@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  template: `
    <div class="fixed inset-0 flex items-center justify-center bg-black bg-opacity-50 z-50">
      <div class="bg-blue-600 rounded w-96 p-6 relative">
        <button (click)="close.emit()" class="absolute top-2 right-4 text-white text-xl">&times;</button>
        
        <h2 class="text-xl text-white mb-4 text-center">Iniciar Sesión</h2>
        
        <form [formGroup]="loginForm" (ngSubmit)="onSubmit()">
          <div class="mb-4">
            <label for="username" class="block text-white mb-1">Usuario / Correo</label>
            <input 
              type="text" 
              id="username" 
              formControlName="username"
              class="w-full px-3 py-2 bg-white text-black rounded border-none"
              placeholder="Ingresa tu usuario">
          </div>
          
          <div class="mb-4">
            <label for="password" class="block text-white mb-1">Contraseña</label>
            <input 
              type="password" 
              id="password" 
              formControlName="password"
              class="w-full px-3 py-2 bg-white text-black rounded border-none"
              placeholder="Ingresa tu contraseña">
          </div>
          
          <div *ngIf="errorMessage" class="mb-4 text-white bg-red-500 p-2 rounded text-center">
            {{ errorMessage }}
          </div>
          
          <button 
            type="submit" 
            [disabled]="loginForm.invalid"
            class="w-full bg-white text-blue-600 py-2 rounded mt-2">
            Entrar
          </button>
          
          <div class="text-center mt-4">
            <button 
              type="button" 
              (click)="goToRegister.emit()"
              class="text-white underline">
              ¿No tienes cuenta? Regístrate
            </button>
          </div>
        </form>
      </div>
    </div>
  `
})
export class LoginComponent {
  @Output() close = new EventEmitter<void>();
  @Output() goToRegister = new EventEmitter<void>();
  
  loginForm: FormGroup;
  errorMessage = '';

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {
    this.loginForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  onSubmit() {
    if (this.loginForm.valid) {
      const { username, password } = this.loginForm.value;
      if (this.authService.login(username, password)) {
        this.close.emit();
        this.router.navigate(['/dashboard']);
      } else {
        this.errorMessage = 'Credenciales inválidas';
      }
    }
  }
}
