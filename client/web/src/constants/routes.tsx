import type { JSX } from "react";
import {
  IconDashboard,
  IconDeviceGamepad3,
  IconMessageReply,
  IconDeviceVisionPro,
  IconUsers,
  IconArticle,
} from "@tabler/icons-react";
import ListArticles from "@/features/articles/pages/list-articles";
import Dashboard from "@/features/dashboard/dashboard";
import ListFeedbacks from "@/features/feedbacks/pages/list-feedbacks";
import ListMembers from "@/features/members/pages/list-members";
import ListServices from "@/features/services/pages/list-services";
import ListTestimonials from "@/features/testimonials/pages/list-testimonial";

export type RouteItem = {
  name: string;
  path: string;
  description?: string;
  icon?: JSX.Element;
  element: JSX.Element;
  children?: RouteItem[];
  isPublic?: boolean;
};

export const routes: RouteItem[] = [
  {
    name: "Dashboard",
    path: "/",
    icon: <IconDashboard />,
    element: <Dashboard />,
  },
  {
    name: "Services",
    path: "/services",
    description: "All Services are listed here",
    icon: <IconDeviceGamepad3 />,
    element: <ListServices />,
  },
  {
    name: "Articles",
    path: "/articles",
    description: "All Articles are listed here",
    icon: <IconArticle />,
    element: <ListArticles />,
  },
  {
    name: "Members",
    path: "/members",
    description: "All Members are listed here",
    icon: <IconUsers />,
    element: <ListMembers />,
  },
  {
    name: "Feedbacks",
    path: "/feedbacks",
    description: "All Feedbacks are listed here",
    icon: <IconMessageReply />,
    element: <ListFeedbacks />,
  },
  {
    name: "Testimonial",
    path: "/testimonials",
    description: "All Testimonials are listed here",
    icon: <IconDeviceVisionPro />,
    element: <ListTestimonials />,
  },
];
