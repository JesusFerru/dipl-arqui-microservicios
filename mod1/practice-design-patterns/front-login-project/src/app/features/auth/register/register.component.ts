import { Component, EventEmitter, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../../core/auth/auth.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  template: `
    <div class="fixed inset-0 flex items-center justify-center bg-black bg-opacity-50 z-50">
      <div class="bg-blue-600 rounded w-96 p-6 relative">
        <button (click)="close.emit()" class="absolute top-2 right-4 text-white text-xl">&times;</button>
        
        <h2 class="text-xl text-white mb-4 text-center">Registro de Usuario</h2>
        
        <form [formGroup]="registerForm" (ngSubmit)="onSubmit()">
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
          
          <div *ngIf="successMessage" class="mb-4 text-white bg-green-500 p-2 rounded text-center">
            {{ successMessage }}
          </div>
          
          <button 
            type="submit" 
            [disabled]="registerForm.invalid"
            class="w-full bg-white text-blue-600 py-2 rounded mt-2">
            Registrarse
          </button>
          
          <div class="text-center mt-4">
            <button 
              type="button" 
              (click)="goToLogin.emit()"
              class="text-white underline">
              ¿Ya tienes cuenta? Inicia sesión
            </button>
          </div>
        </form>
      </div>
    </div>
  `
})
export class RegisterComponent {
  @Output() close = new EventEmitter<void>();
  @Output() goToLogin = new EventEmitter<void>();
  
  registerForm: FormGroup;
  errorMessage = '';
  successMessage = '';

  constructor(
    private fb: FormBuilder,
    private authService: AuthService
  ) {
    this.registerForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }

  onSubmit() {
    this.errorMessage = '';
    this.successMessage = '';
    
    if (this.registerForm.valid) {
      const { username, password } = this.registerForm.value;
      const result = this.authService.register(username, password);
      
      if (result.success) {
        this.successMessage = 'Usuario registrado exitosamente.';
        this.registerForm.reset();
        setTimeout(() => {
          this.goToLogin.emit();
        }, 1500);
      } else {
        this.errorMessage = result.message || 'Error al registrar usuario';
      }
    }
  }
}
