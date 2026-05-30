import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { LoggerService } from '../logging/logger.service';

export interface User {
  username: string;
  passwordHash: string;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly CURRENT_USER_KEY = 'current_user';
  private readonly USERS_KEY = 'app_users';
  
  private authState = new BehaviorSubject<boolean>(this.hasActiveSession());

  constructor(private logger: LoggerService) {
    this.initializeMockUser();
  }

  private initializeMockUser() {
    const usersStr = localStorage.getItem(this.USERS_KEY);
    if (!usersStr) {
      const mockUser: User = {
        username: 'lferrufino',
        passwordHash: this.simpleHash('admin123')
      };
      localStorage.setItem(this.USERS_KEY, JSON.stringify([mockUser]));
    }
  }

  private simpleHash(text: string): string {
    let hash = 0;
    for (let i = 0; i < text.length; i++) {
      const char = text.charCodeAt(i);
      hash = ((hash << 5) - hash) + char;
      hash = hash & hash; // Convert to 32bit integer
    }
    return hash.toString();
  }

  private hasActiveSession(): boolean {
    return !!sessionStorage.getItem(this.CURRENT_USER_KEY);
  }

  isAuthenticated(): Observable<boolean> {
    return this.authState.asObservable();
  }
  
  getIsAuthenticatedValue(): boolean {
    return this.authState.value;
  }

  login(username: string, password: string): boolean {
    this.logger.log(`Intento de login para usuario: ${username}`);
    
    const usersStr = localStorage.getItem(this.USERS_KEY);
    const users: User[] = usersStr ? JSON.parse(usersStr) : [];
    
    const user = users.find(u => u.username === username);
    
    if (user && user.passwordHash === this.simpleHash(password)) {
      sessionStorage.setItem(this.CURRENT_USER_KEY, username);
      this.authState.next(true);
      this.logger.log('Login exitoso');
      return true;
    }
    
    this.logger.log('Acceso denegado: Credenciales inválidas');
    return false;
  }

  register(username: string, password: string): { success: boolean, message?: string } {
    this.logger.log(`Intento de registro para nuevo usuario: ${username}`);
    
    const usersStr = localStorage.getItem(this.USERS_KEY);
    const users: User[] = usersStr ? JSON.parse(usersStr) : [];
    
    if (users.find(u => u.username === username)) {
      this.logger.log(`Fallo de registro: El usuario ${username} ya existe`);
      return { success: false, message: 'El usuario ya existe' };
    }
    
    const newUser: User = {
      username,
      passwordHash: this.simpleHash(password)
    };
    
    users.push(newUser);
    localStorage.setItem(this.USERS_KEY, JSON.stringify(users));
    this.logger.log(`Registro exitoso para usuario: ${username}`);
    return { success: true };
  }

  logout(): void {
    const currentUser = sessionStorage.getItem(this.CURRENT_USER_KEY);
    sessionStorage.removeItem(this.CURRENT_USER_KEY);
    this.authState.next(false);
    this.logger.log(`Logout de usuario: ${currentUser}`);
  }
}
