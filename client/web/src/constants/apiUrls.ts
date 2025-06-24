import type { ArticleFilterParams } from "@/constants/urlTypes";

const BASE_API_URL = import.meta.env.VITE_API_V1_URL
const BASE_URL = import.meta.env.VITE_BASE_URL

export class ApiUrls {
  static loginUrl() {
    return `${BASE_API_URL}/auth/login/web`;
  }

  static refreshTokensUrl() {
    return `${BASE_API_URL}/auth/refresh-tokens/web`;
  }

  static logoutUrl() {
    return `${BASE_API_URL}/auth/logout/web`;
  }

  static getAllServicesUrl() {
    return `${BASE_API_URL}/services/all`;
  }

  static addSerivceUrl() {
    return `${BASE_API_URL}/service`;
  }

  static updateServiceUrl(id: number) {
    return `${BASE_API_URL}/service/${id}`;
  }

  static getAllFeedbackUrl() {
    return `${BASE_API_URL}/feedbacks`;
  }

  static getAllTestimonialsUrl() {
    return `${BASE_API_URL}/testimonials`;
  }

  static getAllMembersUrl() {
    return `${BASE_API_URL}/members`;
  }

  static addMemberUrl() {
    return `${BASE_API_URL}/member`;
  }

  static updateMemberUrl(id: number) {
    return `${BASE_API_URL}/member/${id}`;
  }

  static getAllArticlesUrl(filters: ArticleFilterParams) {
    const params = new URLSearchParams({
      count: filters.count.toString(),
      page: filters.page.toString(),
    });

    if (filters.search) params.append("search", filters.search);
    if (filters.to) params.append("to", filters.to);
    if (filters.from) params.append("from", filters.from);
    if (filters.approval)
      params.append("approval", filters.approval.toString());

    return `${BASE_API_URL}/articles/?${params}`;
  }

  static addArticleUrl() {
    return `${BASE_API_URL}/article`;
  }

  static getArticleByIdUrl(id: number) {
    return `${BASE_API_URL}/article/${id}`;
  }

  static changeApprovalStatusUrl(id: number) {
    return `${BASE_API_URL}/article/${id}/change-approval-status`;
  }
}

export class Urls {
  static serviceImage(imageName: string) {
    return `${BASE_URL}/uploads/services/${imageName}`;
  }

  static memberImage(imageName: string) {
    return `${BASE_URL}/uploads/members/${imageName}`;
  }

  static articleImage(imageName: string) {
    return `${BASE_URL}/uploads/articles/${imageName}`;
  }

  static dpImage(imageName: string) {
    return `${BASE_URL}/uploads/display-pictures/${imageName}`;
  }
}
